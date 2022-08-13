using AdminProjectsDemo.Entitites;

namespace AdminProjectsDemo.Services.Activities
{
    public interface IActivityHandler
    {
        Task<Activity[]> GetAsync();
        Task<Activity> GetByIdAsync(int activityId);
        Task<int> CreateAsync(Activity activity);
        Task UpdateAsync(Activity activity);
        Task<bool> DeleteAsync(int activityId);
        Task<bool> ExistRecordAsync(int activityId);
    }
}
