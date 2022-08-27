using AdminProjectsDemo.DataContext;
using AdminProjectsDemo.Entitites;
using Microsoft.EntityFrameworkCore;

namespace AdminProjectsDemo.Services.ProjectBeneficiary
{
    public class ProjectBeneficiaryHandler : IProjectBeneficiaryHandler
    {
        private readonly ApplicationDbContext _context;

        public ProjectBeneficiaryHandler(ApplicationDbContext context) => this._context = context;

        public async Task<ProyectoBeneficiario[]> GetAsync()
        {
            var projectBeneficiariesDb = this._context.ProyectosBeneficiarios
                                                  .Include(x => x.Proyecto)
                                                    .ThenInclude(y => y.Actividades)
                                                  .Include(x => x.Beneficiario)
                                                  .ToArray();

            return await Task.FromResult(projectBeneficiariesDb);
        }

        public async Task<int> CreateAsync(ProyectoBeneficiario projectBeneficiary)
        {
            if (projectBeneficiary == null)
                return 0;

            this._context.ProyectosBeneficiarios.Add(projectBeneficiary);
            var rowAffected = await this._context.SaveChangesAsync();

            return rowAffected;
        }

        public async Task<ProyectoBeneficiario> GetByProjectIdAndBeneficiaryIdAsync(int projectId, int beneficiaryId)
        {
            var projectBeneficiary = await this._context.ProyectosBeneficiarios
                                                    .Include(x => x.Proyecto)
                                                        .ThenInclude(y => y.Actividades)
                                                    .Include(x => x.Beneficiario)
                                                    .FirstOrDefaultAsync(x => x.ProyectoID == projectId && x.BeneficiarioID == beneficiaryId);

            return await Task.FromResult(projectBeneficiary);
        }
    }
}
