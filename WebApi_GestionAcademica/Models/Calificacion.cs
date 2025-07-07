using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi_GestionAcademica.Models
{
    [Table("Calificaciones")]  // <--- Esta línea es clave
    public class Calificacion
    {
        [Key]
        public int CalificacionID { get; set; }

        [Required]
        public int AsignacionID { get; set; }

        [ForeignKey("AsignacionID")]
        public virtual Asignacion Asignacion { get; set; }

        [Range(0, 10)]
        public decimal? Nota1 { get; set; }

        [Range(0, 100)]
        public int? Asistencia1 { get; set; }

        [Range(0, 10)]
        public decimal? Nota2 { get; set; }

        [Range(0, 100)]
        public int? Asistencia2 { get; set; }

        [Range(0, 10)]
        public decimal? Suplementario { get; set; }

        [StringLength(255)]
        public string Observacion { get; set; }
    }
}
