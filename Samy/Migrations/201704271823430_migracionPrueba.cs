namespace Samy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migracionPrueba : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.SCategorias", newName: "SubCategorias");
            AddColumn("dbo.Productos", "SCategoriaId", c => c.Int(nullable: true));
            CreateIndex("dbo.Productos", "SCategoriaId");
            AddForeignKey("dbo.Productos", "SCategoriaId", "dbo.SubCategorias", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Productos", "SCategoriaId", "dbo.SubCategorias");
            DropIndex("dbo.Productos", new[] { "SCategoriaId" });
            DropColumn("dbo.Productos", "SCategoriaId");
            RenameTable(name: "dbo.SubCategorias", newName: "SCategorias");
        }
    }
}
