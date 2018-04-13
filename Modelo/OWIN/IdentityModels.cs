using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using Modelo.Modelos;
using System.ComponentModel.DataAnnotations.Schema;

namespace Samy.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        [NotMapped]
        public bool IsChecked { get; set; }
        //public int ExamenUsuarioId { get; set; }
        public virtual ICollection<ExamenUsuario> ExamenUsuarios { get; set; }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("Samy", throwIfV1Schema: false)
        {
            Database.SetInitializer<ApplicationDbContext>(null);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Modelo.Producto> Productos { get; set; }

        public System.Data.Entity.DbSet<Modelo.Modelos.SCategoria> SubCategorias { get; set; }

        public System.Data.Entity.DbSet<Modelo.Modelos.Categoria> Categorias { get; set; }

        public System.Data.Entity.DbSet<Modelo.Modelos.Proveedor> Proveedors { get; set; }

        public System.Data.Entity.DbSet<Modelo.Modelos.Alumno> Alumnos { get; set; }

        public System.Data.Entity.DbSet<Modelo.Modelos.TipoDocumento> TipoDocumentos { get; set; }

        public System.Data.Entity.DbSet<Modelo.Modelos.Profesor> Profesors { get; set; }

        public System.Data.Entity.DbSet<Modelo.Modelos.Acudiente> Acudientes { get; set; }

        public System.Data.Entity.DbSet<Modelo.Modelos.Relacion> Relacions { get; set; }

        public System.Data.Entity.DbSet<Modelo.Modelos.AcudienteAlumno> AcudienteAlumnos { get; set; }

        public System.Data.Entity.DbSet<Modelo.Modelos.Pregunta> Preguntas { get; set; }

        public System.Data.Entity.DbSet<Modelo.Modelos.Examen> Examens { get; set; }

        public System.Data.Entity.DbSet<Modelo.Modelos.ExamenPregunta> ExamenPregunta { get; set; }

        public System.Data.Entity.DbSet<Modelo.Modelos.ExamenUsuario> ExamenUsuarios { get; set; }

        public System.Data.Entity.DbSet<Modelo.Modelos.Sede> Sedes { get; set; }
    }


}