using System.ComponentModel.DataAnnotations;

namespace AdminProjectsDemo.DTOs.ProjectsBeneficiaries.Request
{
    public class ProjectBeneficiaryCreationRequest
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int ProyectoID { get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int BeneficiarioID { get; set; }
    }
}
