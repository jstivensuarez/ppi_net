using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelos
{
    public class ExamenAlumno
    {
        [Key]
        public int Id { get; set; }

        public int ExamenId { get; set; }

        public int AlumnoId { get; set; }

        public int Nota { get; set; }

        public virtual Examen Examen { get; set; }

        public virtual Alumno Alumno { get; set; }
    }
}
