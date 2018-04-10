using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using Samy.Models;

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

        [NotMapped]
        public List<ExamenPregunta> ExamenPreguntas { get; set; }
        
        public List<int> AlumnosSelected { get; set; }

        [NotMapped]
        public List<Pregunta> Preguntas { get; set; }

        public List<int> PreguntasSelected { get; set; }

        [NotMapped]
        public List<ApplicationUser> Usuarios { get; set; }

        public List<int> UsuariosSelected { get; set; }

        [NotMapped]
        public Alumno Alumno { get; set; }

        [NotMapped]
        public Pregunta Pregunta { get; set; }


        [NotMapped]
        public Pregunta Examen { get; set; }

        [NotMapped]
        public bool Calificado { get; set; }

    }
}
