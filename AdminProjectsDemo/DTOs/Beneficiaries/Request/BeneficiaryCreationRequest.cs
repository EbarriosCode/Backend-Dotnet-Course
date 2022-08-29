using System.ComponentModel.DataAnnotations;

namespace AdminProjectsDemo.DTOs.Beneficiaries.Request
{
    public class BeneficiaryCreationRequest
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(150, ErrorMessage = "El campo {0} solo puede contener 150 caracteres")]
        public string Nombre { get; set; }

        [StringLength(250, ErrorMessage = "El campo {0} solo puede contener 250 caracteres")]
        public string Descripcion { get; set; }
    }
}
