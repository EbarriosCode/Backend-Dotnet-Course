using System.ComponentModel.DataAnnotations;

namespace AdminProjectsDemo.DTOs.Request
{
    public class RolCreateRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
