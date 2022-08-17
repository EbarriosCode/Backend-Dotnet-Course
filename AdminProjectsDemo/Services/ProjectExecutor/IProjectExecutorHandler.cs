using AdminProjectsDemo.Entitites;

namespace AdminProjectsDemo.Services.ProjectExecutor
{
    public interface IProjectExecutorHandler
    {
        Task<Entitites.ProyectoEjecutor[]> GetAsync();
        Task<Entitites.ProyectoEjecutor> GetByProjectIdAndExecutorIdAsync(int projectId, int executorId);
        Task<int> CreateAsync(Entitites.ProyectoEjecutor executor);
    }
}
