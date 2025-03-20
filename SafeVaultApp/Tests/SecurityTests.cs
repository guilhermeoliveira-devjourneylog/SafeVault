using NUnit.Framework;
using SafeVaultApp.Helpers;
using System.Text.RegularExpressions;

namespace SafeVaultApp.Tests
{
    [TestFixture]
    public class SecurityTests
    {
        [Test]
        public void TestForSQLInjection()
        {
            string maliciousInput = "'; DROP TABLE Users; --";
            bool isValid = IsValidInput(maliciousInput);
            Assert.That(isValid, Is.False, "SQL Injection should be blocked.");
        }

        [Test]
        public void TestForXSS()
        {
            string maliciousInput = "<script>alert('Hacked');</script>";
            bool isValid = IsValidInput(maliciousInput);
            Assert.That(isValid, Is.False, "XSS attack should be blocked.");
        }

        private bool IsValidInput(string input)
        {
            return !string.IsNullOrWhiteSpace(input) && Regex.IsMatch(input, "^[a-zA-Z0-9]+$");
        }

        public void TestXssInput()
        {
            string maliciousInput = "<script>alert('XSS');</script>";
            bool isValid = XSSProtection.IsValidXSSInput(maliciousInput);
            Console.WriteLine(isValid ? "XSS Test Failed" : "XSS Test Passed");
        }

    }
}
