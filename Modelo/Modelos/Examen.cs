using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelos
{
    public class Examen
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha del examen")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaExamen { get; set; }

        [NotMapped]
        public int EstadoExamen { get; set; }

        public bool Calificado { get; set; }

        public string Descripcion { get; set; }

        public virtual ICollection<ExamenPregunta> ExamenPreguntas { get; set; }

        public virtual ICollection<ExamenUsuario> ExamenUsuarios { get; set; }

        public virtual ICollection<ExamenAlumno> ExamenAlumnos { get; set; }
    }
}
