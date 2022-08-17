using AdminProjectsDemo.Entitites;

namespace AdminProjectsDemo.Services.Projects
{
    public interface IProjectHandler
    {
        Task<Proyecto[]> GetAsync();
        Task<Proyecto> GetByIdAsync(int projectId);
        Task<int> CreateAsync(Proyecto project);
        Task UpdateAsync(Proyecto project);
        Task<bool> DeleteAsync(int projectId);
        Task<bool> ExistRecordAsync(int projectId);
    }
}
