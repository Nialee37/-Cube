namespace ServiceDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Adresses", "IdVille", "dbo.Villes");
            DropForeignKey("dbo.Personnes", "Roles_Id", "dbo.Roles");
            DropForeignKey("dbo.Ressources", "Type_Id", "dbo.Types");
            DropForeignKey("dbo.Commentaires", "Personne_Id", "dbo.Personnes");
            DropForeignKey("dbo.Commentaires", "Ressources_Id", "dbo.Ressources");
            DropIndex("dbo.Adresses", new[] { "IdVille" });
            DropIndex("dbo.Personnes", new[] { "Roles_Id" });
            DropIndex("dbo.Ressources", new[] { "Type_Id" });
            DropIndex("dbo.Commentaires", new[] { "Personne_Id" });
            DropIndex("dbo.Commentaires", new[] { "Ressources_Id" });
            DropColumn("dbo.Personnes", "IdRoles");
            DropColumn("dbo.Ressources", "IdType");
            DropColumn("dbo.Commentaires", "IdPersonne");
            DropColumn("dbo.Commentaires", "IdRessource");
            RenameColumn(table: "dbo.Adresses", name: "IdVille", newName: "Ville_IdVille");
            RenameColumn(table: "dbo.Personnes", name: "Roles_Id", newName: "IdRoles");
            RenameColumn(table: "dbo.Ressources", name: "Type_Id", newName: "IdType");
            RenameColumn(table: "dbo.Commentaires", name: "Personne_Id", newName: "IdPersonne");
            RenameColumn(table: "dbo.Commentaires", name: "Ressources_Id", newName: "IdRessource");
            AddColumn("dbo.Ressources", "IdSource", c => c.Int(nullable: false));
            AlterColumn("dbo.Adresses", "Ville_IdVille", c => c.Int());
            AlterColumn("dbo.Personnes", "IdRoles", c => c.Int(nullable: false));
            AlterColumn("dbo.Ressources", "IdType", c => c.Int(nullable: false));
            AlterColumn("dbo.Commentaires", "IdPersonne", c => c.Int(nullable: false));
            AlterColumn("dbo.Commentaires", "IdRessource", c => c.Int(nullable: false));
            CreateIndex("dbo.Adresses", "Ville_IdVille");
            CreateIndex("dbo.Personnes", "IdRoles");
            CreateIndex("dbo.Ressources", "IdType");
            CreateIndex("dbo.Ressources", "IdSource");
            CreateIndex("dbo.Commentaires", "IdPersonne");
            CreateIndex("dbo.Commentaires", "IdRessource");
            AddForeignKey("dbo.Ressources", "IdSource", "dbo.Categories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Adresses", "Ville_IdVille", "dbo.Villes", "IdVille");
            AddForeignKey("dbo.Personnes", "IdRoles", "dbo.Roles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Ressources", "IdType", "dbo.Types", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Commentaires", "IdPersonne", "dbo.Personnes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Commentaires", "IdRessource", "dbo.Ressources", "Id", cascadeDelete: true);
            DropColumn("dbo.Personnes", "IsConnected");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Personnes", "IsConnected", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Commentaires", "IdRessource", "dbo.Ressources");
            DropForeignKey("dbo.Commentaires", "IdPersonne", "dbo.Personnes");
            DropForeignKey("dbo.Ressources", "IdType", "dbo.Types");
            DropForeignKey("dbo.Personnes", "IdRoles", "dbo.Roles");
            DropForeignKey("dbo.Adresses", "Ville_IdVille", "dbo.Villes");
            DropForeignKey("dbo.Ressources", "IdSource", "dbo.Categories");
            DropIndex("dbo.Commentaires", new[] { "IdRessource" });
            DropIndex("dbo.Commentaires", new[] { "IdPersonne" });
            DropIndex("dbo.Ressources", new[] { "IdSource" });
            DropIndex("dbo.Ressources", new[] { "IdType" });
            DropIndex("dbo.Personnes", new[] { "IdRoles" });
            DropIndex("dbo.Adresses", new[] { "Ville_IdVille" });
            AlterColumn("dbo.Commentaires", "IdRessource", c => c.Int());
            AlterColumn("dbo.Commentaires", "IdPersonne", c => c.Int());
            AlterColumn("dbo.Ressources", "IdType", c => c.Int());
            AlterColumn("dbo.Personnes", "IdRoles", c => c.Int());
            AlterColumn("dbo.Adresses", "Ville_IdVille", c => c.Int(nullable: false));
            DropColumn("dbo.Ressources", "IdSource");
            RenameColumn(table: "dbo.Commentaires", name: "IdRessource", newName: "Ressources_Id");
            RenameColumn(table: "dbo.Commentaires", name: "IdPersonne", newName: "Personne_Id");
            RenameColumn(table: "dbo.Ressources", name: "IdType", newName: "Type_Id");
            RenameColumn(table: "dbo.Personnes", name: "IdRoles", newName: "Roles_Id");
            RenameColumn(table: "dbo.Adresses", name: "Ville_IdVille", newName: "IdVille");
            AddColumn("dbo.Commentaires", "IdRessource", c => c.Int(nullable: false));
            AddColumn("dbo.Commentaires", "IdPersonne", c => c.Int(nullable: false));
            AddColumn("dbo.Ressources", "IdType", c => c.Int(nullable: false));
            AddColumn("dbo.Personnes", "IdRoles", c => c.Int(nullable: false));
            CreateIndex("dbo.Commentaires", "Ressources_Id");
            CreateIndex("dbo.Commentaires", "Personne_Id");
            CreateIndex("dbo.Ressources", "Type_Id");
            CreateIndex("dbo.Personnes", "Roles_Id");
            CreateIndex("dbo.Adresses", "IdVille");
            AddForeignKey("dbo.Commentaires", "Ressources_Id", "dbo.Ressources", "Id");
            AddForeignKey("dbo.Commentaires", "Personne_Id", "dbo.Personnes", "Id");
            AddForeignKey("dbo.Ressources", "Type_Id", "dbo.Types", "Id");
            AddForeignKey("dbo.Personnes", "Roles_Id", "dbo.Roles", "Id");
            AddForeignKey("dbo.Adresses", "IdVille", "dbo.Villes", "IdVille", cascadeDelete: true);
        }
    }
}
