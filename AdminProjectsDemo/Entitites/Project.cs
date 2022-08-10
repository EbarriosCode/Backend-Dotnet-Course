using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminProjectsDemo.Entitites
{
    [Table("Projects")]
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public decimal Budget { get; set; }

        [StringLength(150)]
        public string Scope { get; set; }
    }
}
