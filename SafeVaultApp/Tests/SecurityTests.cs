using NUnit.Framework;
using SafeVaultApp.Helpers;
using SafeVaultApp.Services;

namespace SafeVaultApp.Tests
{
    [TestFixture]
    public class SecurityTests
    {
        [Test]
        public void TestForSQLInjection()
        {
            string maliciousInput = "'; DROP TABLE Users; --";
            bool isValid = ValidationHelpers.IsValidInput(maliciousInput);
            Assert.That(isValid, Is.False, "SQL Injection should be blocked.");
        }

        [Test]
        public void TestForXSS()
        {
            string maliciousInput = "<script>alert('Hacked');</script>";
            string sanitizedInput = XSSProtection.SanitizeInput(maliciousInput);
            Assert.That(sanitizedInput, Does.Not.Contain("<script>"), "XSS attack should be neutralized.");
        }

        [Test]
        public void TestValidUsername()
        {
            string validInput = "User123";
            bool isValid = ValidationHelpers.IsValidInput(validInput);
            Assert.That(isValid, Is.True, "Valid username should pass.");
        }

        [Test]
        public void TestValidSanitizedOutput()
        {
            string maliciousInput = "<script>alert('XSS');</script>";
            string sanitizedInput = XSSProtection.SanitizeInput(maliciousInput);
            Assert.That(sanitizedInput, Does.Not.Contain("<script>"), "Sanitization should remove script tags.");
        }
    }

    [TestFixture]
    public class AuthTests
    {
        private IConfiguration _configuration;
        private AuthService _authService;

        [SetUp]
        public void Setup()
        {
            var inMemorySettings = new Dictionary<string, string>
            {
                { "ConnectionStrings:DefaultConnection", "server=localhost;database=safevaultdb;user=root;password=yourpassword" }
            };

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _authService = new AuthService(_configuration);
        }

        [Test]
        public void HashPassword_ShouldCreateHashedPassword()
        {
            string password = "SecurePass123!";
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            Assert.That(hashedPassword, Is.Not.Empty, "Password hashing should produce a non-empty hash.");
        }

        [Test]
        public void VerifyPassword_ShouldReturnTrueForCorrectPassword()
        {
            string password = "SecurePass123!";
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            bool isVerified = BCrypt.Net.BCrypt.Verify(password, hashedPassword);

            Assert.That(isVerified, Is.True, "Correct password should be verified successfully.");
        }

        [Test]
        public void VerifyPassword_ShouldReturnFalseForIncorrectPassword()
        {
            string correctPassword = "SecurePass123!";
            string wrongPassword = "WrongPassword!";
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(correctPassword);

            bool isVerified = BCrypt.Net.BCrypt.Verify(wrongPassword, hashedPassword);

            Assert.That(isVerified, Is.False, "Incorrect password should not be verified.");
        }

        [Test]
        public void ValidateEmail_ShouldReturnTrueForValidEmail()
        {
            string email = "test@example.com";
            bool isValid = ValidationHelpers.IsValidEmail(email);

            Assert.That(isValid, Is.True, "Valid email should pass validation.");
        }

        [Test]
        public void ValidateEmail_ShouldReturnFalseForInvalidEmail()
        {
            string email = "invalid-email.com";
            bool isValid = ValidationHelpers.IsValidEmail(email);

            Assert.That(isValid, Is.False, "Invalid email should fail validation.");
        }
    }
}
