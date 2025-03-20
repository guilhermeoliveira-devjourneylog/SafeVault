using System.ComponentModel.DataAnnotations;

namespace SafeVaultApp.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required, StringLength(50)]
        public string Username { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required, StringLength(20)]
        public string Role { get; set; } 
    }
}
