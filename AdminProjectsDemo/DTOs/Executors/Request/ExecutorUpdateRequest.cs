using System.ComponentModel.DataAnnotations;

namespace AdminProjectsDemo.DTOs.Executors.Request
{
    public class ExecutorUpdateRequest : ExecutorCreationRequest
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int EjecutorID { get; set; }
    }
}
