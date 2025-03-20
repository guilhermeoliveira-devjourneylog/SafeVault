using System;
using System.Web;

namespace SafeVaultApp.Helpers
{
    public static class XSSProtection
    {
        public static string SanitizeInput(string input)
        {
            return HttpUtility.HtmlEncode(input); 
        }
    }
}
