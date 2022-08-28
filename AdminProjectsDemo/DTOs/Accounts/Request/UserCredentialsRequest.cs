using System.ComponentModel.DataAnnotations;

namespace AdminProjectsDemo.DTOs.Accounts.Request
{
    public class UserCredentialsRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
