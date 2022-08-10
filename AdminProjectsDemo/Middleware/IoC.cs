using AdminProjectsDemo.Services.Projects;

namespace AdminProjectsDemo.Middleware
{
    public static class IoC
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            
            services.AddTransient<IProjectHandler, ProjectHandler>();

            return services;
        }
    }
}
