using Microsoft.AspNetCore.Mvc;
using SafeVaultApp.Models;
using SafeVaultApp.Helpers;
using System.Text.RegularExpressions;
using BCrypt.Net;

namespace SafeVaultApp.Controllers
{
    [Route("SafeVault")]
    public class SafeVaultController : Controller
    {
        private readonly DatabaseContext _context;

        public SafeVaultController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("Registration")]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost("submit")]
        public IActionResult Submit(string username, string email, string password, string confirmPassword)
        {
            if (!IsValidUsername(username) || !ValidationHelpers.IsValidEmail(email))
            {
                ViewData["Message"] = "Invalid input detected.";
                return View("Registration");
            }

            if (password != confirmPassword)
            {
                ViewData["Message"] = "Passwords do not match.";
                return View("Registration");
            }

            if (_context.Users.Any(u => u.Email == email))
            {
                ViewData["Message"] = "Email is already registered.";
                return View("Registration");
            }

            var sanitizedUsername = XSSProtection.SanitizeInput(username);
            var sanitizedEmail = XSSProtection.SanitizeInput(email);
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User
            {
                Username = sanitizedUsername,
                Email = sanitizedEmail,
                PasswordHash = hashedPassword,
                Role = "User"
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            ViewData["Message"] = "User successfully registered.";
            return View("Registration");
        }

        private bool IsValidUsername(string username)
        {
            return !string.IsNullOrWhiteSpace(username) && Regex.IsMatch(username, "^[a-zA-Z0-9]+$");
        }
    }
}
