using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelos
{
    public class Sede
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Sede")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Nombre { get; set; }

        [NotMapped]
        public bool IsChecked { get; set; }

        public virtual ICollection<Alumno> Alumnos { get; set; }
    }
}
