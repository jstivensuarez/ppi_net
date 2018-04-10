using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelos
{
    public class Pregunta
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Descripción")]
        public string Descripcion { get; set; }
        [Display(Name = "Categoría")]
        public int CategoriaId { get; set; }

        [NotMapped]
        public bool IsChecked { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(0, 10, ErrorMessage = "La nota debe estar entre el rango (0 y 10)")]
        public int Nota { get; set; }

        public Categoria Categoria { get; set; }

        public virtual ICollection<ExamenPregunta> ExamenPreguntas { get; set; }

    }
}
