namespace Samy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11072017users : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExamenUsuarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExamenId = c.Int(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Examen", t => t.ExamenId, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.ExamenId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Correo = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExamenUsuarios", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.ExamenUsuarios", "ExamenId", "dbo.Examen");
            DropIndex("dbo.ExamenUsuarios", new[] { "UsuarioId" });
            DropIndex("dbo.ExamenUsuarios", new[] { "ExamenId" });
            DropTable("dbo.Usuarios");
            DropTable("dbo.ExamenUsuarios");
        }
    }
}
