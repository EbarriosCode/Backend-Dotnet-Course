using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminProjectsDemo.Entitites
{
    [Table("ProjectsExecutors")]
    public class ProjectExecutor
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public int ExecutorId { get; set; }
        public Executor Executor { get; set; }
    }
}
