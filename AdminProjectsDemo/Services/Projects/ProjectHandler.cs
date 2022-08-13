using AdminProjectsDemo.DataContext;
using AdminProjectsDemo.Entitites;
using Microsoft.EntityFrameworkCore;

namespace AdminProjectsDemo.Services.Projects
{
    public class ProjectHandler : IProjectHandler
    {
        private readonly ApplicationDbContext _context;

        public ProjectHandler(ApplicationDbContext context) => this._context = context;        

        public async Task<Project[]> GetAsync()
        {                     
            var projectsDb = this._context.Projects.Include(x => x.Activities).ToArray();

            return await Task.FromResult(projectsDb);
        }

        public async Task<Project> GetByIdAsync(int projectId)
        {
            var project = await this._context.Projects.FirstOrDefaultAsync(x => x.ProjectId == projectId);

            return await Task.FromResult(project);
        }

        public async Task<int> CreateAsync(Project project)
        {
            if(project == null)
                return 0;

            this._context.Projects.Add(project);
            var projectId = await this._context.SaveChangesAsync();

            return projectId;
        }

        public async Task UpdateAsync(Project project)
        {
            this._context.Entry(project).State = EntityState.Modified;
            await this._context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int projectId)
        {
            var existProject = await this._context.Projects.AnyAsync(x => x.ProjectId == projectId);

            if(!existProject)
                return false;

            var projectToDelete = new Project() { ProjectId = projectId };
            
            this._context.Projects.Remove(projectToDelete);
            await this._context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ExistRecordAsync(int projectId)
        {
            if (projectId <= 0)
                throw new ArgumentException("Invalid projectId");

            bool existRecord = await this._context.Projects.AnyAsync(x =>x.ProjectId == projectId);

            return existRecord;
        }
    }
}
