using System.ComponentModel.DataAnnotations;

namespace AdminProjectsDemo.DTOs.Roles.Request
{
    public class RolCreationRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
