using NUnit.Framework;
using SafeVaultApp.Helpers;

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
}
