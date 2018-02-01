using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

public class SamyContext : DbContext
{
    // You can add custom code to this file. Changes will not be overwritten.
    // 
    // If you want Entity Framework to drop and regenerate your database
    // automatically whenever you change your model schema, please use data migrations.
    // For more information refer to the documentation:
    // http://msdn.microsoft.com/en-us/data/jj591621.aspx

    public SamyContext() : base("name=Samy")
    {
        Database.SetInitializer<SamyContext>(null);
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

    public System.Data.Entity.DbSet<Modelo.Modelos.ExamenAlumno> ExamenAlumnos { get; set; }

    public System.Data.Entity.DbSet<Modelo.Modelos.Usuario> Usuarios { get; set; }

    public System.Data.Entity.DbSet<Modelo.Modelos.Sede> Sedes { get; set; }

}
