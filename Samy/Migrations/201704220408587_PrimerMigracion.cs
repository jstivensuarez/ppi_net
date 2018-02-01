namespace Samy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrimerMigracion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Proveedores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Email = c.String(),
                        Direccion = c.String(),
                        Telefono = c.String(),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Productos", "ProveedorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Productos", "ProveedorId");
            AddForeignKey("dbo.Productos", "ProveedorId", "dbo.Proveedores", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Productos", "ProveedorId", "dbo.Proveedores");
            DropIndex("dbo.Productos", new[] { "ProveedorId" });
            DropColumn("dbo.Productos", "ProveedorId");
            DropTable("dbo.Proveedores");
        }
    }
}
