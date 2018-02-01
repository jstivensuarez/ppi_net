namespace Samy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Prue : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Examen", "Descripcion", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Examen", "Descripcion");
        }
    }
}
