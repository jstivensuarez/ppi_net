using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelos
{
    public class ExamenPregunta
    {
        [Key]
        public int Id { get; set; }

        public int ExamenId { get; set; }

        public int PreguntaId { get; set; }

        public virtual Pregunta Pregunta { get; set; }

        public virtual Examen Examen { get; set; }
    }
}
