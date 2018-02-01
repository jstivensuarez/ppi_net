using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Dto
{
    public class AcudienteDto
    {
        public Acudiente Acudiente { get; set; }

        public List<RelacionAlumno> Relaciones { get; set; }

        public Alumno Alumno { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Tipo de documento")]
        public int TipoDocumentoId { get; set; }
    }
}
