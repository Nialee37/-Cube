namespace ServiceDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Adresses",
                c => new
                    {
                        IdAdresse = c.Int(nullable: false, identity: true),
                        Numero = c.String(),
                        Nom = c.String(),
                        Type = c.Int(nullable: false),
                        IdVille = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdAdresse)
                .ForeignKey("dbo.Villes", t => t.IdVille, cascadeDelete: true)
                .Index(t => t.IdVille);
            
            CreateTable(
                "dbo.Villes",
                c => new
                    {
                        IdVille = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        CPostal = c.String(),
                    })
                .PrimaryKey(t => t.IdVille);
            
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
                "dbo.Personnes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Prenom = c.String(),
                        DateNaissance = c.DateTime(nullable: false),
                        Genre = c.Int(nullable: false),
                        PasswordHash = c.String(),
                        Mail = c.String(),
                        IdAdresse = c.Int(nullable: false),
                        IdRoles = c.Int(nullable: false),
                        IsConnected = c.Boolean(nullable: false),
                        Roles_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Adresses", t => t.IdAdresse, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Roles_Id)
                .Index(t => t.IdAdresse)
                .Index(t => t.Roles_Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Libelle = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ressources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Date = c.DateTime(nullable: false),
                        CheminAcces = c.String(),
                        Source = c.String(),
                        IdType = c.Int(nullable: false),
                        Type_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Types", t => t.Type_Id)
                .Index(t => t.Type_Id);
            
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
            
            CreateTable(
                "dbo.Historiques",
                c => new
                    {
                        IdPersonne = c.Int(nullable: false),
                        IdRessource = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Personne_Id = c.Int(),
                        Ressources_Id = c.Int(),
                    })
                .PrimaryKey(t => new { t.IdPersonne, t.IdRessource })
                .ForeignKey("dbo.Personnes", t => t.Personne_Id)
                .ForeignKey("dbo.Ressources", t => t.Ressources_Id)
                .Index(t => t.Personne_Id)
                .Index(t => t.Ressources_Id);
            
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
            DropForeignKey("dbo.Historiques", "Ressources_Id", "dbo.Ressources");
            DropForeignKey("dbo.Historiques", "Personne_Id", "dbo.Personnes");
            DropForeignKey("dbo.Favoris", "Ressources_Id", "dbo.Ressources");
            DropForeignKey("dbo.Favoris", "Personne_Id", "dbo.Personnes");
            DropForeignKey("dbo.Commentaires", "Ressources_Id", "dbo.Ressources");
            DropForeignKey("dbo.Commentaires", "Personne_Id", "dbo.Personnes");
            DropForeignKey("dbo.Archives", "Ressources_Id", "dbo.Ressources");
            DropForeignKey("dbo.Ressources", "Type_Id", "dbo.Types");
            DropForeignKey("dbo.Archives", "Personne_Id", "dbo.Personnes");
            DropForeignKey("dbo.Personnes", "Roles_Id", "dbo.Roles");
            DropForeignKey("dbo.Personnes", "IdAdresse", "dbo.Adresses");
            DropForeignKey("dbo.Adresses", "IdVille", "dbo.Villes");
            DropIndex("dbo.LinkRessCats", new[] { "Ressources_Id" });
            DropIndex("dbo.LinkRessCats", new[] { "Categorie_Id" });
            DropIndex("dbo.Historiques", new[] { "Ressources_Id" });
            DropIndex("dbo.Historiques", new[] { "Personne_Id" });
            DropIndex("dbo.Favoris", new[] { "Ressources_Id" });
            DropIndex("dbo.Favoris", new[] { "Personne_Id" });
            DropIndex("dbo.Commentaires", new[] { "Ressources_Id" });
            DropIndex("dbo.Commentaires", new[] { "Personne_Id" });
            DropIndex("dbo.Ressources", new[] { "Type_Id" });
            DropIndex("dbo.Personnes", new[] { "Roles_Id" });
            DropIndex("dbo.Personnes", new[] { "IdAdresse" });
            DropIndex("dbo.Archives", new[] { "Ressources_Id" });
            DropIndex("dbo.Archives", new[] { "Personne_Id" });
            DropIndex("dbo.Adresses", new[] { "IdVille" });
            DropTable("dbo.LinkRessCats");
            DropTable("dbo.Historiques");
            DropTable("dbo.Favoris");
            DropTable("dbo.Commentaires");
            DropTable("dbo.Categories");
            DropTable("dbo.Types");
            DropTable("dbo.Ressources");
            DropTable("dbo.Roles");
            DropTable("dbo.Personnes");
            DropTable("dbo.Archives");
            DropTable("dbo.Villes");
            DropTable("dbo.Adresses");
        }
    }
}
