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
                    })
                .PrimaryKey(t => new { t.IdPersonne, t.IdRessource })
                .ForeignKey("dbo.Personnes", t => t.IdPersonne, cascadeDelete: true)
                .ForeignKey("dbo.Ressources", t => t.IdRessource, cascadeDelete: true)
                .Index(t => t.IdPersonne)
                .Index(t => t.IdRessource);
            
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
                        IconProfile = c.String(),
                        Mail = c.String(),
                        IdAdresse = c.Int(nullable: false),
                        IdRoles = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Adresses", t => t.IdAdresse, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.IdRoles, cascadeDelete: true)
                .Index(t => t.IdAdresse)
                .Index(t => t.IdRoles);
            
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
                        IdCategorie = c.Int(nullable: false),
                        IdPersonne = c.Int(nullable: false),
                        IsValidate = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.IdCategorie, cascadeDelete: true)
                .ForeignKey("dbo.Personnes", t => t.IdPersonne)
                .ForeignKey("dbo.Types", t => t.IdType, cascadeDelete: true)
                .Index(t => t.IdType)
                .Index(t => t.IdCategorie)
                .Index(t => t.IdPersonne);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Libelle = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Types",
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
                        commentaire = c.String(),
                        IdPersonne = c.Int(nullable: false),
                        IdRessource = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Personnes", t => t.IdPersonne, cascadeDelete: true)
                .ForeignKey("dbo.Ressources", t => t.IdRessource, cascadeDelete: true)
                .Index(t => t.IdPersonne)
                .Index(t => t.IdRessource);
            
            CreateTable(
                "dbo.Favoris",
                c => new
                    {
                        IdPersonne = c.Int(nullable: false),
                        IdRessource = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdPersonne, t.IdRessource })
                .ForeignKey("dbo.Personnes", t => t.IdPersonne, cascadeDelete: true)
                .ForeignKey("dbo.Ressources", t => t.IdRessource, cascadeDelete: true)
                .Index(t => t.IdPersonne)
                .Index(t => t.IdRessource);
            
            CreateTable(
                "dbo.Historiques",
                c => new
                    {
                        IdPersonne = c.Int(nullable: false),
                        IdRessource = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdPersonne, t.IdRessource })
                .ForeignKey("dbo.Personnes", t => t.IdPersonne, cascadeDelete: true)
                .ForeignKey("dbo.Ressources", t => t.IdRessource, cascadeDelete: true)
                .Index(t => t.IdPersonne)
                .Index(t => t.IdRessource);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Historiques", "IdRessource", "dbo.Ressources");
            DropForeignKey("dbo.Historiques", "IdPersonne", "dbo.Personnes");
            DropForeignKey("dbo.Favoris", "IdRessource", "dbo.Ressources");
            DropForeignKey("dbo.Favoris", "IdPersonne", "dbo.Personnes");
            DropForeignKey("dbo.Commentaires", "IdRessource", "dbo.Ressources");
            DropForeignKey("dbo.Commentaires", "IdPersonne", "dbo.Personnes");
            DropForeignKey("dbo.Archives", "IdRessource", "dbo.Ressources");
            DropForeignKey("dbo.Ressources", "IdType", "dbo.Types");
            DropForeignKey("dbo.Ressources", "IdPersonne", "dbo.Personnes");
            DropForeignKey("dbo.Ressources", "IdCategorie", "dbo.Categories");
            DropForeignKey("dbo.Archives", "IdPersonne", "dbo.Personnes");
            DropForeignKey("dbo.Personnes", "IdRoles", "dbo.Roles");
            DropForeignKey("dbo.Personnes", "IdAdresse", "dbo.Adresses");
            DropForeignKey("dbo.Adresses", "IdVille", "dbo.Villes");
            DropIndex("dbo.Historiques", new[] { "IdRessource" });
            DropIndex("dbo.Historiques", new[] { "IdPersonne" });
            DropIndex("dbo.Favoris", new[] { "IdRessource" });
            DropIndex("dbo.Favoris", new[] { "IdPersonne" });
            DropIndex("dbo.Commentaires", new[] { "IdRessource" });
            DropIndex("dbo.Commentaires", new[] { "IdPersonne" });
            DropIndex("dbo.Ressources", new[] { "IdPersonne" });
            DropIndex("dbo.Ressources", new[] { "IdCategorie" });
            DropIndex("dbo.Ressources", new[] { "IdType" });
            DropIndex("dbo.Personnes", new[] { "IdRoles" });
            DropIndex("dbo.Personnes", new[] { "IdAdresse" });
            DropIndex("dbo.Archives", new[] { "IdRessource" });
            DropIndex("dbo.Archives", new[] { "IdPersonne" });
            DropIndex("dbo.Adresses", new[] { "IdVille" });
            DropTable("dbo.Historiques");
            DropTable("dbo.Favoris");
            DropTable("dbo.Commentaires");
            DropTable("dbo.Types");
            DropTable("dbo.Categories");
            DropTable("dbo.Ressources");
            DropTable("dbo.Roles");
            DropTable("dbo.Personnes");
            DropTable("dbo.Archives");
            DropTable("dbo.Villes");
            DropTable("dbo.Adresses");
        }
    }
}
