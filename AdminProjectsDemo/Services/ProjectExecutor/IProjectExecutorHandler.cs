using AdminProjectsDemo.Entitites;

namespace AdminProjectsDemo.Services.ProjectExecutor
{
    public interface IProjectExecutorHandler
    {
        Task<Entitites.ProjectExecutor[]> GetAsync();
        Task<Entitites.ProjectExecutor> GetByProjectIdAndExecutorIdAsync(int projectId, int executorId);
        Task<int> CreateAsync(Entitites.ProjectExecutor executor);
    }
}
