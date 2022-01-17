namespace ServiceDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Adresses", "Ville_IdVille", "dbo.Villes");
            DropIndex("dbo.Adresses", new[] { "Ville_IdVille" });
            RenameColumn(table: "dbo.Adresses", name: "Ville_IdVille", newName: "IdVille");
            AlterColumn("dbo.Adresses", "IdVille", c => c.Int(nullable: false));
            CreateIndex("dbo.Adresses", "IdVille");
            AddForeignKey("dbo.Adresses", "IdVille", "dbo.Villes", "IdVille", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Adresses", "IdVille", "dbo.Villes");
            DropIndex("dbo.Adresses", new[] { "IdVille" });
            AlterColumn("dbo.Adresses", "IdVille", c => c.Int());
            RenameColumn(table: "dbo.Adresses", name: "IdVille", newName: "Ville_IdVille");
            CreateIndex("dbo.Adresses", "Ville_IdVille");
            AddForeignKey("dbo.Adresses", "Ville_IdVille", "dbo.Villes", "IdVille");
        }
    }
}
