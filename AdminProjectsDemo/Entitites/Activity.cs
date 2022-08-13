using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminProjectsDemo.Entitites
{
    [Table("Activities")]
    public class Activity
    {
        [Key]
        public int ActivityId { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        
        [StringLength(250)]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(65,2)")]
        public decimal Cost { get; set; }

        [Required]
        [StringLength(100)]
        public string ResponsibleName { get; set; }
    }
}
