using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Modelo
{
    [Table(name:"Productos")]
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        public decimal Precio { get; set; }

        public decimal Costo { get; set; }

        public string Nombre { get; set; }

        public DateTime FechaDeCompra { get; set; }

        public string Imagen { get; set; }

        public int ProveedorId { get; set; }

        public int SCategoriaId { get; set; }

        public char Genero { get; set; }

        [NotMapped]
        public HttpPostedFileBase file { get; set; }

        public virtual Proveedor Proveedor { get; set; }

        public virtual SCategoria SCategoria { get; set; }
    }
}
