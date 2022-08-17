using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminProjectsDemo.Entitites
{
    [Table("ProyectosEjecutores")]
    public class ProyectoEjecutor
    {
        [Column("Proyecto_Id")]
        public int ProyectoID { get; set; }
        public Proyecto Proyecto { get; set; }

        [Column("Ejecutor_Id")]
        public int EjecutorID { get; set; }
        public Ejecutor Ejecutor { get; set; }
    }
}
