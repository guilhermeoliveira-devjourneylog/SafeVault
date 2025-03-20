using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using SafeVaultApp.Models;
using SafeVaultApp.Helpers;

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
            if (!ValidationHelpers.IsValidInput(username) || !ValidationHelpers.IsValidEmail(email))
            {
                return BadRequest("Invalid input detected.");
            }

            var user = new User { Username = username, Email = email };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("User successfully registered.");
        }
    }
}
