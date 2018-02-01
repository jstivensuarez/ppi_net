namespace Samy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Productos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Costo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Nombre = c.String(),
                        FechaDeCompra = c.DateTime(nullable: false),
                        Imagen = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Productos");
        }
    }
}
