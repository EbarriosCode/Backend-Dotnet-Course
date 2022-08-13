using AdminProjectsDemo.Services.Activities;
using AdminProjectsDemo.Services.Executors;
using AdminProjectsDemo.Services.Projects;

namespace AdminProjectsDemo.Middleware
{
    public static class IoC
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            services.AddTransient<IProjectHandler, ProjectHandler>();
            services.AddTransient<IActivityHandler, ActivityHandler>();
            services.AddTransient<IExecutorHandler, ExecutorHandler>();

            return services;
        }
    }
}
