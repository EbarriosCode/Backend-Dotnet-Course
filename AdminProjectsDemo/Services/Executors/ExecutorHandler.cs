using AdminProjectsDemo.DataContext;
using AdminProjectsDemo.Entitites;
using Microsoft.EntityFrameworkCore;

namespace AdminProjectsDemo.Services.Executors
{
    public class ExecutorHandler : IExecutorHandler
    {
        private readonly ApplicationDbContext _context;

        public ExecutorHandler(ApplicationDbContext context) => this._context = context;

        public async Task<Executor[]> GetAsync()
        {
            var executorsDb = this._context.Executors.ToArray();

            return await Task.FromResult(executorsDb);
        }

        public async Task<Executor> GetByIdAsync(int executorId)
        {
            var executor = await this._context.Executors.FirstOrDefaultAsync(x => x.ExecutorId == executorId);

            return await Task.FromResult(executor);
        }

        public async Task<int> CreateAsync(Executor executor)
        {
            if (executor == null)
                return 0;

            this._context.Executors.Add(executor);
            var rowAffected = await this._context.SaveChangesAsync();

            return rowAffected;
        }

        public async Task UpdateAsync(Executor executor)
        {
            this._context.Entry(executor).State = EntityState.Modified;
            await this._context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int executorId)
        {
            var existExecutor = await this._context.Executors.AnyAsync(x => x.ExecutorId == executorId);

            if (!existExecutor)
                return false;

            var executorToDelete = new Executor() { ExecutorId = executorId };

            this._context.Executors.Remove(executorToDelete);
            await this._context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ExistRecordAsync(int executorId)
        {
            if (executorId <= 0)
                throw new ArgumentException("Invalid ExecutorId");

            bool existRecord = await this._context.Executors.AnyAsync(x => x.ExecutorId == executorId);

            return existRecord;
        }
    }
}