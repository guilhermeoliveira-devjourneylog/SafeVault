using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SafeVaultApp.Models;
using SafeVaultApp.Helpers;
using System.Text.RegularExpressions;

namespace SafeVaultApp.Controllers
{
    public class SafeVaultController : Controller
    {
        private readonly DatabaseContext _context;

        public SafeVaultController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("submit")]
        public IActionResult Submit(string username, string email)
        {
            if (!IsValidUsername(username) || !ValidationHelpers.IsValidEmail(email))
            {
                return BadRequest("Invalid input detected.");
            }

            var sanitizedUsername = XSSProtection.SanitizeInput(username);
            var sanitizedEmail = XSSProtection.SanitizeInput(email);

            var user = new User { Username = sanitizedUsername, Email = sanitizedEmail };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("User successfully registered.");
        }

        private bool IsValidUsername(string username)
        {
            return !string.IsNullOrWhiteSpace(username) && Regex.IsMatch(username, "^[a-zA-Z0-9]+$");
        }
    }
}
