using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelos
{
    public class Relacion
    {
        public int Id { get; set; }
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Descripcion { get; set; }

        public virtual ICollection<AcudienteAlumno> AcudienteAlumnos { get; set; }
    }
}
