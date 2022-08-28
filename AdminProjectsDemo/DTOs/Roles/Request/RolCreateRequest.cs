using System.ComponentModel.DataAnnotations;

namespace AdminProjectsDemo.DTOs.Roles.Request
{
    public class RolCreateRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
