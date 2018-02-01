namespace Samy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmigrationppi : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Sedes",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Nombre = c.String(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //AddColumn("dbo.Alumnoes", "SedeId", c => c.Int(nullable: false));
            AlterColumn("dbo.Categorias", "Descripcion", c => c.String(nullable: false));
            //CreateIndex("dbo.Alumnoes", "SedeId");
            //AddForeignKey("dbo.Alumnoes", "SedeId", "dbo.Sedes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.Alumnoes", "SedeId", "dbo.Sedes");
            //DropIndex("dbo.Alumnoes", new[] { "SedeId" });
            AlterColumn("dbo.Categorias", "Descripcion", c => c.String());
            //DropColumn("dbo.Alumnoes", "SedeId");
            //DropTable("dbo.Sedes");
        }
    }
}
