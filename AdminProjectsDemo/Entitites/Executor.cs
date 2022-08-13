using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminProjectsDemo.Entitites
{
    [Table("Executors")]
    public class Executor
    {
        [Key]
        public int ExecutorId { get; set; }
        
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
    }
}
