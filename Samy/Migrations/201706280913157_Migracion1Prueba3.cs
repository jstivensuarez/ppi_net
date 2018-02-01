namespace Samy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migracion1Prueba3 : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.Alumnoes", "TipoDocumento_Id", "dbo.TipoDocumentoes");
            //DropIndex("dbo.Alumnoes", new[] { "TipoDocumento_Id" });
            //RenameColumn(table: "dbo.Alumnoes", name: "TipoDocumento_Id", newName: "TipoDocumentoId");
            //AlterColumn("dbo.Alumnoes", "TipoDocumentoId", c => c.Int(nullable: false));
            //CreateIndex("dbo.Alumnoes", "TipoDocumentoId");
            //AddForeignKey("dbo.Alumnoes", "TipoDocumentoId", "dbo.TipoDocumentoes", "Id", cascadeDelete: true);
            //DropColumn("dbo.Alumnoes", "TipoDocumendoId");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.Alumnoes", "TipoDocumendoId", c => c.Int(nullable: false));
            //DropForeignKey("dbo.Alumnoes", "TipoDocumentoId", "dbo.TipoDocumentoes");
            //DropIndex("dbo.Alumnoes", new[] { "TipoDocumentoId" });
            //AlterColumn("dbo.Alumnoes", "TipoDocumentoId", c => c.Int());
            //RenameColumn(table: "dbo.Alumnoes", name: "TipoDocumentoId", newName: "TipoDocumento_Id");
            //CreateIndex("dbo.Alumnoes", "TipoDocumento_Id");
            //AddForeignKey("dbo.Alumnoes", "TipoDocumento_Id", "dbo.TipoDocumentoes", "Id");
        }
    }
}
