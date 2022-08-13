using AdminProjectsDemo.Services.Activities;
using AdminProjectsDemo.Services.Projects;

namespace AdminProjectsDemo.Middleware
{
    public static class IoC
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            services.AddTransient<IProjectHandler, ProjectHandler>();
            services.AddTransient<IActivityHandler, ActivityHandler>();

            return services;
        }
    }
}
