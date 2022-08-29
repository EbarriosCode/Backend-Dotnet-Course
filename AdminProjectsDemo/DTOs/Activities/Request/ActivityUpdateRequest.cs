using System.ComponentModel.DataAnnotations;

namespace AdminProjectsDemo.DTOs.Activities.Request
{
    public class ActivityUpdateRequest : ActivityCreationRequest
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int ActividadID { get; set; }
    }
}
