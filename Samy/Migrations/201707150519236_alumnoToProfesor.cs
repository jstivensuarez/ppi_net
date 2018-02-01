namespace Samy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alumnoToProfesor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Profesors", "IsAlumno", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Profesors", "IsAlumno");
        }
    }
}
