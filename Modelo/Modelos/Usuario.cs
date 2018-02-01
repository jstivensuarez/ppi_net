
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelos
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Nombre { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Ingrese un {0} válido")]
        public string Correo { get; set; }

        [NotMapped]
        public bool IsChecked { get; set; }

        public virtual ICollection<ExamenUsuario> ExamenUsuarios { get; set; }
    }
}
