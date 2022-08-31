using AdminProjectsDemo.Services.Activities;
using AdminProjectsDemo.Services.Beneficiaries;
using AdminProjectsDemo.Services.Executors;
using AdminProjectsDemo.Services.ProjectBeneficiary;
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
            services.AddTransient<IBeneficiaryHandler, BeneficiaryHandler>();
            services.AddTransient<IProjectBeneficiaryHandler, ProjectBeneficiaryHandler>();

            return services;
        }
    }
}
