using AdminProjectsDemo.DataContext;
using AdminProjectsDemo.Entitites;
using Microsoft.EntityFrameworkCore;

namespace AdminProjectsDemo.Services.Beneficiaries
{
    public class BeneficiaryHandler : IBeneficiaryHandler
    {
        private readonly ApplicationDbContext _context;

        public BeneficiaryHandler(ApplicationDbContext context) => this._context = context;

        public async Task<Beneficiario[]> GetAsync()
        {
            var beneficiariesDb = this._context.Beneficiarios.ToArray();

            return await Task.FromResult(beneficiariesDb);
        }

        public async Task<Beneficiario> GetByIdAsync(int beneficiaryId)
        {
            var beneficiary = await this._context.Beneficiarios.FirstOrDefaultAsync(x => x.BeneficiarioID == beneficiaryId);

            return await Task.FromResult(beneficiary);
        }

        public async Task<int> CreateAsync(Beneficiario beneficiary)
        {
            if (beneficiary == null)
                return 0;

            this._context.Beneficiarios.Add(beneficiary);
            var rowAffected = await this._context.SaveChangesAsync();

            return rowAffected;
        }

        public async Task UpdateAsync(Beneficiario beneficiary)
        {
            this._context.Entry(beneficiary).State = EntityState.Modified;
            await this._context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int beneficiaryId)
        {
            var beneficiaryToDelete = new Beneficiario() { BeneficiarioID = beneficiaryId };

            this._context.Beneficiarios.Remove(beneficiaryToDelete);
            var rowAffected = await this._context.SaveChangesAsync();

            return rowAffected > 0;
        }

        public async Task<bool> ExistRecordAsync(int beneficiaryId)
        {
            bool existRecord = await this._context.Beneficiarios.AnyAsync(x => x.BeneficiarioID == beneficiaryId);

            return existRecord;
        }
    }
}
