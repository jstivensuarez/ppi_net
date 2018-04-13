using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelos
{
    public class Alumno
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

        [Display(Name = "Fecha de nacimiento")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaNacimiento { get; set; }
        [NotMapped]
        public DateTime FechaNacimientoAux { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Categoría")]
        public int CategoriaId { get; set; }

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

        [Display(Name = "Sede")]
        public int? SedeId { get; set; }

        public virtual Categoria Categoria { get; set; }

        public virtual TipoDocumento TipoDocumento { get; set; }

        public virtual Sede Sede { get; set; }

        public virtual ICollection<AcudienteAlumno> AcudienteAlumnos { get; set; }

        [NotMapped]
        public bool Calificado { get; set; }

        [NotMapped]
        public bool IsChecked { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return Nombre+" "+PrimerApellido+" "+SegundoApellido;
            }
        }

    }
}
