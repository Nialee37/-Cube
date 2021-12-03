namespace ServiceDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LinkRessCats",
                c => new
                    {
                        IdCategorie = c.Int(nullable: false),
                        IdRessource = c.Int(nullable: false),
                        Categorie_Id = c.Int(),
                        Ressources_Id = c.Int(),
                    })
                .PrimaryKey(t => new { t.IdCategorie, t.IdRessource })
                .ForeignKey("dbo.Categories", t => t.Categorie_Id)
                .ForeignKey("dbo.Ressources", t => t.Ressources_Id)
                .Index(t => t.Categorie_Id)
                .Index(t => t.Ressources_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LinkRessCats", "Ressources_Id", "dbo.Ressources");
            DropForeignKey("dbo.LinkRessCats", "Categorie_Id", "dbo.Categories");
            DropIndex("dbo.LinkRessCats", new[] { "Ressources_Id" });
            DropIndex("dbo.LinkRessCats", new[] { "Categorie_Id" });
            DropTable("dbo.LinkRessCats");
        }
    }
}
