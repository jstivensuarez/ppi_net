namespace Samy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prueba : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Alumnoes", "FechaNacimiento", c => c.DateTime(nullable: false));
            DropColumn("dbo.Alumnoes", "Edad");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Alumnoes", "Edad", c => c.Int(nullable: false));
            DropColumn("dbo.Alumnoes", "FechaNacimiento");
        }
    }
}
