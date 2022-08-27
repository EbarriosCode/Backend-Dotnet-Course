using AdminProjectsDemo.Entitites;

namespace AdminProjectsDemo.Services.ProjectExecutor
{
    public interface IProjectExecutorHandler
    {
        Task<ProyectoEjecutor[]> GetAsync();
        Task<ProyectoEjecutor> GetByProjectIdAndExecutorIdAsync(int projectId, int executorId);
        Task<int> CreateAsync(ProyectoEjecutor projectExecutor);
    }
}
