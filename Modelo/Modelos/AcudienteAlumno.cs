using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelos
{
    public class AcudienteAlumno
    {
        public int Id { get; set; }

        public int AlumnoId { get; set; }

        public int AcudienteId { get; set; }

        public int RelacionId { get; set; }

        public virtual Acudiente Acudiente { get; set; }

        public virtual Alumno Alumno { get; set; }

        public virtual Relacion Relacion { get; set; }
    }
}
