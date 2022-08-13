using AdminProjectsDemo.Entitites;

namespace AdminProjectsDemo.Services.Executors
{
    public interface IExecutorHandler
    {
        Task<Executor[]> GetAsync();
        Task<Executor> GetByIdAsync(int executorId);
        Task<int> CreateAsync(Executor executor);
        Task UpdateAsync(Executor executor);
        Task<bool> DeleteAsync(int executorId);
        Task<bool> ExistRecordAsync(int executorId);
    }
}
