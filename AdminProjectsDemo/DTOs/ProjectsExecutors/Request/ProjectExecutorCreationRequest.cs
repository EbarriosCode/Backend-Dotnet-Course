using System.ComponentModel.DataAnnotations;

namespace AdminProjectsDemo.DTOs.ProjectsExecutors.Request
{
    public class ProjectExecutorCreationRequest
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int ProyectoID { get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int EjecutorID { get; set; }
    }
}
