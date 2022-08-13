using AdminProjectsDemo.Entitites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdminProjectsDemo.DataContext
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Executor> Executors { get; set; }
        public DbSet<ProjectExecutor> ProjectsExecutors { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ProjectExecutor>().HasKey(pe => new { pe.ProjectId, pe.ExecutorId });
        }
    }
}
