using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelos
{
    [Table(name:"Proveedores")]
    public class Proveedor
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Email { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public string Url { get; set; }


        public virtual IEnumerable<Producto> Productos { get; set; }
    }
}
