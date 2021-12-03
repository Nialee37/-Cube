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
                "dbo.Personnes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Prenom = c.String(),
                        DateNaissance = c.DateTime(nullable: false),
                        Genre = c.Int(nullable: false),
                        PasswordHash = c.String(),
                        IdAdresse = c.Int(nullable: false),
                        IdRoles = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Adresses", t => t.IdAdresse, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.IdRoles)
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Personnes", "Roles_Id", "dbo.Roles");
            DropForeignKey("dbo.Personnes", "IdAdresse", "dbo.Adresses");
            DropForeignKey("dbo.Adresses", "IdVille", "dbo.Villes");
            DropIndex("dbo.Personnes", new[] { "Roles_Id" });
            DropIndex("dbo.Personnes", new[] { "IdAdresse" });
            DropIndex("dbo.Adresses", new[] { "IdVille" });
            DropTable("dbo.Roles");
            DropTable("dbo.Personnes");
            DropTable("dbo.Villes");
            DropTable("dbo.Adresses");
        }
    }
}
