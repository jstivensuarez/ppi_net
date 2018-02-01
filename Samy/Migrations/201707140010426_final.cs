namespace Samy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class final : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Alumnoes", "SegundoApellido", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Alumnoes", "SegundoApellido", c => c.String(nullable: false));
        }
    }
}
