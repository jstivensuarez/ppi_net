using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelos
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Cinturón")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Descripcion { get; set; }

        [NotMapped]
        public bool IsChecked { get; set; }

        public virtual IEnumerable<SCategoria> SCategorias { get; set; }
        public virtual IEnumerable<Alumno> Alumnos { get; set; }
        public virtual IEnumerable<Pregunta> Preguntas { get; set; }
    }
}
