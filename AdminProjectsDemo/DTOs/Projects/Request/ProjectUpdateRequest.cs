using System.ComponentModel.DataAnnotations;

namespace AdminProjectsDemo.DTOs.Projects.Request
{
    public class ProjectUpdateRequest : ProjectCreationRequest
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int ProyectoID { get; set; }
    }
}
