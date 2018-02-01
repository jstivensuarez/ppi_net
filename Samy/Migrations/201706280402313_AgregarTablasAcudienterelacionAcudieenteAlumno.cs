namespace Samy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregarTablasAcudienterelacionAcudieenteAlumno : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AcudienteAlumnoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AlumnoId = c.Int(nullable: false),
                        AcudienteId = c.Int(nullable: false),
                        RelacionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Acudientes", t => t.AcudienteId, cascadeDelete: true)
                .ForeignKey("dbo.Alumnoes", t => t.AlumnoId, cascadeDelete: true)
                .ForeignKey("dbo.Relacions", t => t.RelacionId, cascadeDelete: true)
                .Index(t => t.AlumnoId)
                .Index(t => t.AcudienteId)
                .Index(t => t.RelacionId);
            
            CreateTable(
                "dbo.Acudientes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Documento = c.String(),
                        Nombre = c.String(),
                        PrimerApellido = c.String(),
                        SegundoApellido = c.String(),
                        Correo = c.String(),
                        Telefono = c.String(),
                        Direccion = c.String(),
                        TipoDocumentoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TipoDocumentoes", t => t.TipoDocumentoId, cascadeDelete: true)
                .Index(t => t.TipoDocumentoId);
            
            CreateTable(
                "dbo.Profesors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Documento = c.String(),
                        Nombre = c.String(),
                        PrimerApellido = c.String(),
                        SegundoApellido = c.String(),
                        Correo = c.String(),
                        Telefono = c.String(),
                        Direccion = c.String(),
                        TipoDocumentoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TipoDocumentoes", t => t.TipoDocumentoId, cascadeDelete: true)
                .Index(t => t.TipoDocumentoId);
            
            CreateTable(
                "dbo.Relacions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AcudienteAlumnoes", "RelacionId", "dbo.Relacions");
            DropForeignKey("dbo.AcudienteAlumnoes", "AlumnoId", "dbo.Alumnoes");
            DropForeignKey("dbo.Profesors", "TipoDocumentoId", "dbo.TipoDocumentoes");
            DropForeignKey("dbo.Acudientes", "TipoDocumentoId", "dbo.TipoDocumentoes");
            DropForeignKey("dbo.AcudienteAlumnoes", "AcudienteId", "dbo.Acudientes");
            DropIndex("dbo.Profesors", new[] { "TipoDocumentoId" });
            DropIndex("dbo.Acudientes", new[] { "TipoDocumentoId" });
            DropIndex("dbo.AcudienteAlumnoes", new[] { "RelacionId" });
            DropIndex("dbo.AcudienteAlumnoes", new[] { "AcudienteId" });
            DropIndex("dbo.AcudienteAlumnoes", new[] { "AlumnoId" });
            DropTable("dbo.Relacions");
            DropTable("dbo.Profesors");
            DropTable("dbo.Acudientes");
            DropTable("dbo.AcudienteAlumnoes");
        }
    }
}
