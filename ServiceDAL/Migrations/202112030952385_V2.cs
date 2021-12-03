namespace ServiceDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Archives",
                c => new
                    {
                        IdPersonne = c.Int(nullable: false),
                        IdRessource = c.Int(nullable: false),
                        Personne_Id = c.Int(),
                        Ressources_Id = c.Int(),
                    })
                .PrimaryKey(t => new { t.IdPersonne, t.IdRessource })
                .ForeignKey("dbo.Personnes", t => t.Personne_Id)
                .ForeignKey("dbo.Ressources", t => t.Ressources_Id)
                .Index(t => t.Personne_Id)
                .Index(t => t.Ressources_Id);
            
            CreateTable(
                "dbo.Types",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Libelle = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Libelle = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Commentaires",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        commentaire = c.Long(nullable: false),
                        IdPersonne = c.Int(nullable: false),
                        IdRessource = c.Int(nullable: false),
                        Personne_Id = c.Int(),
                        Ressources_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Personnes", t => t.Personne_Id)
                .ForeignKey("dbo.Ressources", t => t.Ressources_Id)
                .Index(t => t.Personne_Id)
                .Index(t => t.Ressources_Id);
            
            CreateTable(
                "dbo.Favoris",
                c => new
                    {
                        IdPersonne = c.Int(nullable: false),
                        IdRessource = c.Int(nullable: false),
                        Personne_Id = c.Int(),
                        Ressources_Id = c.Int(),
                    })
                .PrimaryKey(t => new { t.IdPersonne, t.IdRessource })
                .ForeignKey("dbo.Personnes", t => t.Personne_Id)
                .ForeignKey("dbo.Ressources", t => t.Ressources_Id)
                .Index(t => t.Personne_Id)
                .Index(t => t.Ressources_Id);
            
            AddColumn("dbo.Ressources", "IdType", c => c.Int(nullable: false));
            AddColumn("dbo.Ressources", "Type_Id", c => c.Int());
            CreateIndex("dbo.Ressources", "Type_Id");
            AddForeignKey("dbo.Ressources", "Type_Id", "dbo.Types", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Favoris", "Ressources_Id", "dbo.Ressources");
            DropForeignKey("dbo.Favoris", "Personne_Id", "dbo.Personnes");
            DropForeignKey("dbo.Commentaires", "Ressources_Id", "dbo.Ressources");
            DropForeignKey("dbo.Commentaires", "Personne_Id", "dbo.Personnes");
            DropForeignKey("dbo.Archives", "Ressources_Id", "dbo.Ressources");
            DropForeignKey("dbo.Ressources", "Type_Id", "dbo.Types");
            DropForeignKey("dbo.Archives", "Personne_Id", "dbo.Personnes");
            DropIndex("dbo.Favoris", new[] { "Ressources_Id" });
            DropIndex("dbo.Favoris", new[] { "Personne_Id" });
            DropIndex("dbo.Commentaires", new[] { "Ressources_Id" });
            DropIndex("dbo.Commentaires", new[] { "Personne_Id" });
            DropIndex("dbo.Ressources", new[] { "Type_Id" });
            DropIndex("dbo.Archives", new[] { "Ressources_Id" });
            DropIndex("dbo.Archives", new[] { "Personne_Id" });
            DropColumn("dbo.Ressources", "Type_Id");
            DropColumn("dbo.Ressources", "IdType");
            DropTable("dbo.Favoris");
            DropTable("dbo.Commentaires");
            DropTable("dbo.Categories");
            DropTable("dbo.Types");
            DropTable("dbo.Archives");
        }
    }
}
