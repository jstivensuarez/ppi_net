using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Dto
{
    public class RelacionAlumno
    {
        public int AlumnoId { get; set; }

        public int RelacionId { get; set; }

        public Alumno Alumno { get; set; }

    }
}
