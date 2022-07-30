using System.ComponentModel.DataAnnotations;

namespace AdminProjectsDemo.DTOs.Request
{
    public class AssignRoleToUserRequest
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}
