using AdminProjectsDemo.DataContext;
using Microsoft.EntityFrameworkCore;

namespace AdminProjectsDemo.Services.ProjectExecutor
{
    public class ProjectExecutorHandler : IProjectExecutorHandler
    {
        private readonly ApplicationDbContext _context;

        public ProjectExecutorHandler(ApplicationDbContext context) => this._context = context;

        public async Task<Entitites.ProyectoEjecutor[]> GetAsync()
        {
            var projectExecutorsDb = this._context.ProyectosEjecutores
                                                  .Include(x => x.Proyecto)
                                                    .ThenInclude(y => y.Actividades)
                                                  .Include(x => x.Ejecutor)
                                                  .ToArray();

            return await Task.FromResult(projectExecutorsDb);
        }

        public async Task<int> CreateAsync(Entitites.ProyectoEjecutor projectExecutor)
        {
            if (projectExecutor == null)
                return 0;

            this._context.ProyectosEjecutores.Add(projectExecutor);
            var rowAffected = await this._context.SaveChangesAsync();

            return rowAffected;
        }

        public async Task<Entitites.ProyectoEjecutor> GetByProjectIdAndExecutorIdAsync(int projectId, int executorId)
        {
            var projectExecutor = await this._context.ProyectosEjecutores
                                                    .Include(x => x.Proyecto)
                                                        .ThenInclude(y => y.Actividades)
                                                    .Include(x => x.Ejecutor)
                                                    .FirstOrDefaultAsync(x => x.ProyectoID == projectId && x.EjecutorID == executorId);

            return await Task.FromResult(projectExecutor);
        }
    }
}
