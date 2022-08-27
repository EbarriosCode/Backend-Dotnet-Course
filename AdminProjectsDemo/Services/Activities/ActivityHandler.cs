using AdminProjectsDemo.DataContext;
using AdminProjectsDemo.Entitites;
using Microsoft.EntityFrameworkCore;

namespace AdminProjectsDemo.Services.Activities
{
    public class ActivityHandler : IActivityHandler
    {
        private readonly ApplicationDbContext _context;

        public ActivityHandler(ApplicationDbContext context) => this._context = context;        

        public async Task<Actividad[]> GetAsync()
        {                     
            var activitiesDb = this._context.Actividades.ToArray();

            return await Task.FromResult(activitiesDb);
        }

        public async Task<Actividad> GetByIdAsync(int activityId)
        {
            var activity = await this._context.Actividades.FirstOrDefaultAsync(x => x.ActividadID == activityId);

            return await Task.FromResult(activity);
        }

        public async Task<int> CreateAsync(Actividad activity)
        {
            if(activity == null)
                return 0;

            this._context.Actividades.Add(activity);
            var rowAffected = await this._context.SaveChangesAsync();

            return rowAffected;
        }

        public async Task UpdateAsync(Actividad activity)
        {
            this._context.Entry(activity).State = EntityState.Modified;
            await this._context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int activityId)
        {
           var activityToDelete = new Actividad() { ActividadID = activityId };
            
            this._context.Actividades.Remove(activityToDelete);
            var rowAffected = await this._context.SaveChangesAsync();

            return rowAffected > 0;
        }

        public async Task<bool> ExistRecordAsync(int activityId)
        {
            bool existRecord = await this._context.Actividades.AnyAsync(x =>x.ActividadID == activityId);

            return existRecord;
        }
    }
}
