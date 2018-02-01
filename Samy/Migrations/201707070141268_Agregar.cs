namespace Samy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agregar : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Examen", "EstadoExamen");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Examen", "EstadoExamen", c => c.Int(nullable: false));
        }
    }
}
