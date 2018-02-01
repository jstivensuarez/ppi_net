using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Dto
{
    public class ExamenDto
    {
        [Display(Name = "Fecha del examen")]
        [DisplayFormat(ApplyFormatInEditMode = true,DataFormatString = "{0:dd/MM/yyyy}")]
        //[DataType(DataType.Date, ErrorMessage = "Error {0}")]
        public DateTime FechaExamen { get; set; }
        public DateTime FechaExamenAux { get; set; }

        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [NotMapped]
        public List<Alumno> Alumnos { get; set; }

        public List<int> AlumnosSelected { get; set; }

        [NotMapped]
        public List<Pregunta> Preguntas { get; set; }

        public List<int> PreguntasSelected { get; set; }

        [NotMapped]
        public List<Usuario> Usuarios { get; set; }

        public List<int> UsuariosSelected { get; set; }

        [NotMapped]
        public Alumno Alumno { get; set; }

        [NotMapped]
        public Pregunta Pregunta { get; set; }

        [NotMapped]
        public bool terminado { get; set; }

    }
}
