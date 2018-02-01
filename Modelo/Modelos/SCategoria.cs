using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelos
{
    [Table(name: "SubCategorias")]
    public class SCategoria
    {
        [Key]
        public int Id { get; set; }

        public string Descripcion { get; set; }

        public int CategoriaId { get; set; }

        public virtual Categoria Categoria { get; set; }

        public virtual IEnumerable<Producto> Productos { get; set; }
    }
}
