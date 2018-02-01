namespace Samy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregarTablaAlumnos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alumnoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Documento = c.String(),
                        Nombre = c.String(),
                        PrimerApellido = c.String(),
                        SegundoApellido = c.String(),
                        Edad = c.Int(nullable: false),
                        CategoriaId = c.Int(nullable: false),
                        Correo = c.String(),
                        Telefono = c.String(),
                        Direccion = c.String(),
                        TipoDocumendoId = c.Int(nullable: false),
                        TipoDocumento_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categorias", t => t.CategoriaId, cascadeDelete: true)
                .ForeignKey("dbo.TipoDocumentoes", t => t.TipoDocumento_Id)
                .Index(t => t.CategoriaId)
                .Index(t => t.TipoDocumento_Id);
            
            CreateTable(
                "dbo.TipoDocumentoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Alumnoes", "TipoDocumento_Id", "dbo.TipoDocumentoes");
            DropForeignKey("dbo.Alumnoes", "CategoriaId", "dbo.Categorias");
            DropIndex("dbo.Alumnoes", new[] { "TipoDocumento_Id" });
            DropIndex("dbo.Alumnoes", new[] { "CategoriaId" });
            DropTable("dbo.TipoDocumentoes");
            DropTable("dbo.Alumnoes");
        }
    }
}
