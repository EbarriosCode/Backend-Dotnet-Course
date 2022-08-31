using AdminProjectsDemo.DataContext;
using AdminProjectsDemo.Entitites;
using AdminProjectsDemo.Services.Base;
using Microsoft.EntityFrameworkCore;

namespace AdminProjectsDemo.Services.Activities
{
    public class ActivityHandler : BaseService<Actividad>, IActivityHandler
    {
        public ActivityHandler(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        { }
    }
}
