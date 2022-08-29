using System.ComponentModel.DataAnnotations;

namespace AdminProjectsDemo.DTOs.Beneficiaries.Request
{
    public class BeneficiaryUpdateRequest : BeneficiaryCreationRequest
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int BeneficiarioID { get; set; }
    }
}
