using Samy.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelos
{
    public class ExamenUsuario
    {
        [Key]
        public int Id { get; set; }

        public int ExamenId { get; set; }

        public int ApplicationUserID { get; set; }

        public Examen Examen { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
