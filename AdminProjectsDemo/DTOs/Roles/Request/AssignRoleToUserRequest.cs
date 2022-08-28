using System.ComponentModel.DataAnnotations;

namespace AdminProjectsDemo.DTOs.Roles.Request
{
    public class AssignRoleToUserRequest
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}
