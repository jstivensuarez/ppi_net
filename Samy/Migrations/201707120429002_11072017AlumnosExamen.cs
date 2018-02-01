namespace Samy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11072017AlumnosExamen : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExamenAlumnoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExamenId = c.Int(nullable: false),
                        AlumnoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Alumnoes", t => t.AlumnoId, cascadeDelete: true)
                .ForeignKey("dbo.Examen", t => t.ExamenId, cascadeDelete: true)
                .Index(t => t.ExamenId)
                .Index(t => t.AlumnoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExamenAlumnoes", "ExamenId", "dbo.Examen");
            DropForeignKey("dbo.ExamenAlumnoes", "AlumnoId", "dbo.Alumnoes");
            DropIndex("dbo.ExamenAlumnoes", new[] { "AlumnoId" });
            DropIndex("dbo.ExamenAlumnoes", new[] { "ExamenId" });
            DropTable("dbo.ExamenAlumnoes");
        }
    }
}
