﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SafeVaultApp.Helpers
{
    public static class ValidationHelpers
    {
        public static bool IsValidInput(string input, string allowedSpecialCharacters = "")
        {
            if (string.IsNullOrEmpty(input))
                return false;

            var validCharacters = allowedSpecialCharacters.ToHashSet();
            return input.All(c => char.IsLetterOrDigit(c) || validCharacters.Contains(c));
        }

        public static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
    }
}
