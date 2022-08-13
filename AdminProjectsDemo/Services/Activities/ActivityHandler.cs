using AdminProjectsDemo.DataContext;
using AdminProjectsDemo.Entitites;
using Microsoft.EntityFrameworkCore;

namespace AdminProjectsDemo.Services.Activities
{
    public class ActivityHandler : IActivityHandler
    {
        private readonly ApplicationDbContext _context;

        public ActivityHandler(ApplicationDbContext context) => this._context = context;        

        public async Task<Activity[]> GetAsync()
        {                     
            var activitiesDb = this._context.Activities.ToArray();

            return await Task.FromResult(activitiesDb);
        }

        public async Task<Activity> GetByIdAsync(int activityId)
        {
            var activity = await this._context.Activities.FirstOrDefaultAsync(x => x.ActivityId == activityId);

            return await Task.FromResult(activity);
        }

        public async Task<int> CreateAsync(Activity activity)
        {
            if(activity == null)
                return 0;

            this._context.Activities.Add(activity);
            var activityId = await this._context.SaveChangesAsync();

            return activityId;
        }

        public async Task UpdateAsync(Activity activity)
        {
            this._context.Entry(activity).State = EntityState.Modified;
            await this._context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int activityId)
        {
            var existActivity = await this._context.Activities.AnyAsync(x => x.ActivityId == activityId);

            if(!existActivity)
                return false;

            var activityToDelete = new Activity() { ActivityId = activityId };
            
            this._context.Activities.Remove(activityToDelete);
            await this._context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ExistRecordAsync(int activityId)
        {
            if (activityId <= 0)
                throw new ArgumentException("Invalid ActivityId");

            bool existRecord = await this._context.Activities.AnyAsync(x =>x.ActivityId == activityId);

            return existRecord;
        }
    }
}
