namespace Samy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migracion1Prueba34 : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.Alumnoes", "TipoDocumentoId", "dbo.TipoDocumentoes");
            //DropIndex("dbo.Alumnoes", new[] { "TipoDocumentoId" });
            //RenameColumn(table: "dbo.Alumnoes", name: "TipoDocumentoId", newName: "TipoDocumento_Id");
            //AddColumn("dbo.Alumnoes", "TipoDocumendoId", c => c.Int(nullable: false));
            //AlterColumn("dbo.Alumnoes", "TipoDocumento_Id", c => c.Int());
            //CreateIndex("dbo.Alumnoes", "TipoDocumento_Id");
            //AddForeignKey("dbo.Alumnoes", "TipoDocumento_Id", "dbo.TipoDocumentoes", "Id");
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.Alumnoes", "TipoDocumento_Id", "dbo.TipoDocumentoes");
            //DropIndex("dbo.Alumnoes", new[] { "TipoDocumento_Id" });
            //AlterColumn("dbo.Alumnoes", "TipoDocumento_Id", c => c.Int(nullable: false));
            //DropColumn("dbo.Alumnoes", "TipoDocumendoId");
            //RenameColumn(table: "dbo.Alumnoes", name: "TipoDocumento_Id", newName: "TipoDocumentoId");
            //CreateIndex("dbo.Alumnoes", "TipoDocumentoId");
            //AddForeignKey("dbo.Alumnoes", "TipoDocumentoId", "dbo.TipoDocumentoes", "Id", cascadeDelete: true);
        }
    }
}
