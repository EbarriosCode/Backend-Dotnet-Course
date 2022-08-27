using AdminProjectsDemo.Services.Activities;
using AdminProjectsDemo.Services.Executors;
using AdminProjectsDemo.Services.ProjectExecutor;
using AdminProjectsDemo.Services.Projects;

namespace AdminProjectsDemo.InversionOfControlContainer
{
    public static class IoC
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            services.AddTransient<IProjectHandler, ProjectHandler>();
            services.AddTransient<IActivityHandler, ActivityHandler>();
            services.AddTransient<IExecutorHandler, ExecutorHandler>();
            services.AddTransient<IProjectExecutorHandler, ProjectExecutorHandler>();

            return services;
        }
    }
}
