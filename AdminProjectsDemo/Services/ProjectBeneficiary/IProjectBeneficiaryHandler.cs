using AdminProjectsDemo.Entitites;

namespace AdminProjectsDemo.Services.ProjectBeneficiary
{
    public interface IProjectBeneficiaryHandler
    {
        Task<ProyectoBeneficiario[]> GetAsync();
        Task<ProyectoBeneficiario> GetByProjectIdAndBeneficiaryIdAsync(int projectId, int beneficiaryId);
        Task<int> CreateAsync(ProyectoBeneficiario projectBeneficiary);
    }
}
