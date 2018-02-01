namespace Samy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregarExamenP : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExamenPreguntas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExamenId = c.Int(nullable: false),
                        PreguntaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Examen", t => t.ExamenId, cascadeDelete: true)
                .ForeignKey("dbo.Preguntas", t => t.PreguntaId, cascadeDelete: true)
                .Index(t => t.ExamenId)
                .Index(t => t.PreguntaId);
            
            CreateTable(
                "dbo.Examen",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FechaExamen = c.DateTime(nullable: false),
                        EstadoExamen = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Preguntas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExamenPreguntas", "PreguntaId", "dbo.Preguntas");
            DropForeignKey("dbo.ExamenPreguntas", "ExamenId", "dbo.Examen");
            DropIndex("dbo.ExamenPreguntas", new[] { "PreguntaId" });
            DropIndex("dbo.ExamenPreguntas", new[] { "ExamenId" });
            DropTable("dbo.Preguntas");
            DropTable("dbo.Examen");
            DropTable("dbo.ExamenPreguntas");
        }
    }
}
