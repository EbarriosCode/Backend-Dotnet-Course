using AdminProjectsDemo.DataContext;
using AdminProjectsDemo.Entitites;
using AdminProjectsDemo.Services.Base;

namespace AdminProjectsDemo.Services.Projects
{
    public class ProjectHandler : BaseService<Proyecto>, IProjectHandler
    {
        public ProjectHandler(ApplicationDbContext applicationDbContext)
             : base(applicationDbContext)
        { }
    }
}
