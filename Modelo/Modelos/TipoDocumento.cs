using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelos
{
    public class TipoDocumento
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Tipo de documento")]
        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        public string Descripcion { get; set; }
       
        public virtual ICollection<Alumno> Alumnos { get; set; }

        public virtual ICollection<Profesor> Profesores { get; set; }

        public virtual ICollection<Acudiente> Acudientes { get; set; }
    }
}
