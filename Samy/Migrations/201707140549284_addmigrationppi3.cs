namespace Samy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmigrationppi3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Alumnoes", "SedeId", "dbo.Sedes");
            DropIndex("dbo.Alumnoes", new[] { "SedeId" });
            AlterColumn("dbo.Alumnoes", "SedeId", c => c.Int());
            CreateIndex("dbo.Alumnoes", "SedeId");
            AddForeignKey("dbo.Alumnoes", "SedeId", "dbo.Sedes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Alumnoes", "SedeId", "dbo.Sedes");
            DropIndex("dbo.Alumnoes", new[] { "SedeId" });
            AlterColumn("dbo.Alumnoes", "SedeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Alumnoes", "SedeId");
            AddForeignKey("dbo.Alumnoes", "SedeId", "dbo.Sedes", "Id", cascadeDelete: true);
        }
    }
}
