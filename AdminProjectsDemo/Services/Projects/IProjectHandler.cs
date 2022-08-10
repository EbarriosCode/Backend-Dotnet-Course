using AdminProjectsDemo.Entitites;

namespace AdminProjectsDemo.Services.Projects
{
    public interface IProjectHandler
    {
        Task<Project[]> GetAsync();
        Task<Project> GetByIdAsync(int projectId);
        Task<int> CreateAsync(Project project);
        Task UpdateAsync(Project project);
        Task<bool> DeleteAsync(int projectId);
        Task<bool> ExistRecordAsync(int projectId);
    }
}
