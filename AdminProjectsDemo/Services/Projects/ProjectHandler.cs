using AdminProjectsDemo.DataContext;
using AdminProjectsDemo.Entitites;
using Microsoft.EntityFrameworkCore;

namespace AdminProjectsDemo.Services.Projects
{
    public class ProjectHandler : IProjectHandler
    {
        private readonly ApplicationDbContext _context;

        public ProjectHandler(ApplicationDbContext context) => this._context = context;        

        public async Task<Proyecto[]> GetAsync()
        {                     
            var projectsDb = this._context.Proyectos.Include(x => x.Actividades).ToArray();

            return await Task.FromResult(projectsDb);
        }

        public async Task<Proyecto> GetByIdAsync(int projectId)
        {
            var project = await this._context.Proyectos.FirstOrDefaultAsync(x => x.ProyectoID == projectId);

            return await Task.FromResult(project);
        }

        public async Task<int> CreateAsync(Proyecto project)
        {
            if(project == null)
                return 0;

            this._context.Proyectos.Add(project);
            var projectId = await this._context.SaveChangesAsync();

            return projectId;
        }

        public async Task UpdateAsync(Proyecto project)
        {
            this._context.Entry(project).State = EntityState.Modified;
            await this._context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int projectId)
        {
            var existProject = await this._context.Proyectos.AnyAsync(x => x.ProyectoID == projectId);

            if(!existProject)
                return false;

            var projectToDelete = new Proyecto() { ProyectoID = projectId };
            
            this._context.Proyectos.Remove(projectToDelete);
            await this._context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ExistRecordAsync(int projectId)
        {
            if (projectId <= 0)
                throw new ArgumentException("Invalid projectId");

            bool existRecord = await this._context.Proyectos.AnyAsync(x =>x.ProyectoID == projectId);

            return existRecord;
        }
    }
}
