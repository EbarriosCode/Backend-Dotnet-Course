using AdminProjectsDemo.DataContext;
using AdminProjectsDemo.Entitites;
using AdminProjectsDemo.Services.Base;

namespace AdminProjectsDemo.Services.Executors
{
    public class ExecutorHandler : BaseService<Ejecutor>, IExecutorHandler
    {
        public ExecutorHandler(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)    
        { }
    }
}