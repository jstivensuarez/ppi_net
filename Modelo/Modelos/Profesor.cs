using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelos
{
    public class Profesor
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, 99999999999, ErrorMessage = "Error")]
        public string Documento { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Primer apellido")]
        public string PrimerApellido { get; set; }

        [Display(Name = "Segundo apellido")]
        public string SegundoApellido { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Ingrese un {0} válido")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Télefono")]
        [Range(1, 9999999999, ErrorMessage = "Entre {1} y {2}")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Ingrese un {0} válido")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Tipo de documento")]
        public int TipoDocumentoId { get; set; }

        [Display(Name = "Alumno")]
        public bool IsAlumno { get; set; }

        public virtual TipoDocumento TipoDocumento { get; set; }
    }
}
