using AdminProjectsDemo.Entitites;

namespace AdminProjectsDemo.Services.Executors
{
    public interface IExecutorHandler
    {
        Task<Ejecutor[]> GetAsync();
        Task<Ejecutor> GetByIdAsync(int executorId);
        Task<int> CreateAsync(Ejecutor executor);
        Task UpdateAsync(Ejecutor executor);
        Task<bool> DeleteAsync(int executorId);
        Task<bool> ExistRecordAsync(int executorId);
    }
}
