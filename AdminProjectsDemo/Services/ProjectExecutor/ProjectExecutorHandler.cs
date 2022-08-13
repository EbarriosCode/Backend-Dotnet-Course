using AdminProjectsDemo.DataContext;
using Microsoft.EntityFrameworkCore;

namespace AdminProjectsDemo.Services.ProjectExecutor
{
    public class ProjectExecutorHandler : IProjectExecutorHandler
    {
        private readonly ApplicationDbContext _context;

        public ProjectExecutorHandler(ApplicationDbContext context) => this._context = context;

        public async Task<Entitites.ProjectExecutor[]> GetAsync()
        {
            var projectExecutorsDb = this._context.ProjectsExecutors
                                                  .Include(x => x.Project)
                                                    .ThenInclude(y => y.Activities)
                                                  .Include(x => x.Executor)
                                                  .ToArray();

            return await Task.FromResult(projectExecutorsDb);
        }

        public async Task<int> CreateAsync(Entitites.ProjectExecutor projectExecutor)
        {
            if (projectExecutor == null)
                return 0;

            this._context.ProjectsExecutors.Add(projectExecutor);
            var rowAffected = await this._context.SaveChangesAsync();

            return rowAffected;
        }

        public async Task<Entitites.ProjectExecutor> GetByProjectIdAndExecutorIdAsync(int projectId, int executorId)
        {
            var projectExecutor = await this._context.ProjectsExecutors
                                                    .Include(x => x.Project)
                                                        .ThenInclude(y => y.Activities)
                                                    .Include(x => x.Executor)
                                                    .FirstOrDefaultAsync(x => x.ProjectId == projectId && x.ExecutorId == executorId);

            return await Task.FromResult(projectExecutor);
        }
    }
}
