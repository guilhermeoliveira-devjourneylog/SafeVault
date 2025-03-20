using System.ComponentModel.DataAnnotations;

namespace SafeVaultApp.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Required, StringLength(50)]
        public string Username { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
