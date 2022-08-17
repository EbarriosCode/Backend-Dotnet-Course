using AdminProjectsDemo.Entitites;

namespace AdminProjectsDemo.Services.Activities
{
    public interface IActivityHandler
    {
        Task<Actividad[]> GetAsync();
        Task<Actividad> GetByIdAsync(int activityId);
        Task<int> CreateAsync(Actividad activity);
        Task UpdateAsync(Actividad activity);
        Task<bool> DeleteAsync(int activityId);
        Task<bool> ExistRecordAsync(int activityId);
    }
}
