using System.ComponentModel.DataAnnotations;

namespace AdminProjectsDemo.DTOs.Request
{
    public class AccountUpdateRequest
    {
        [Required]
        public string Id { get; set; }        
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
