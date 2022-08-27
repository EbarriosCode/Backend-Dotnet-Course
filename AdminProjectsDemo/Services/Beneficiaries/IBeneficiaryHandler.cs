using AdminProjectsDemo.Entitites;

namespace AdminProjectsDemo.Services.Beneficiaries
{
    public interface IBeneficiaryHandler 
    {
        Task<Beneficiario[]> GetAsync();
        Task<Beneficiario> GetByIdAsync(int beneficiaryId);
        Task<int> CreateAsync(Beneficiario beneficiary);
        Task UpdateAsync(Beneficiario beneficiary);
        Task<bool> DeleteAsync(int beneficiaryId);
        Task<bool> ExistRecordAsync(int beneficiaryId);
    }
}
