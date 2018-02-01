namespace Samy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class categoriasToPreguntas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Preguntas", "CategoriaId", c => c.Int(nullable: true));
            CreateIndex("dbo.Preguntas", "CategoriaId");
            AddForeignKey("dbo.Preguntas", "CategoriaId", "dbo.Categorias", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Preguntas", "CategoriaId", "dbo.Categorias");
            DropIndex("dbo.Preguntas", new[] { "CategoriaId" });
            DropColumn("dbo.Preguntas", "CategoriaId");
        }
    }
}
