using System;

namespace SafeVaultApp.Helpers
{
    public static class XSSProtection
    {
        public static bool IsValidXSSInput(string input)
        {
            if (string.IsNullOrEmpty(input))
                return true;

            string lowerInput = input.ToLower();
            return !(lowerInput.Contains("<script") || lowerInput.Contains("<iframe"));
        }
    }
}
