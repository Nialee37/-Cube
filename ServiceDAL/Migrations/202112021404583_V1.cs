namespace ServiceDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V1 : DbMigration
    {
        public override void Up()
        {
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
                "dbo.Ressources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Date = c.DateTime(nullable: false),
                        CheminAcces = c.String(),
                        Source = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Historiques", "Ressources_Id", "dbo.Ressources");
            DropForeignKey("dbo.Historiques", "Personne_Id", "dbo.Personnes");
            DropIndex("dbo.Historiques", new[] { "Ressources_Id" });
            DropIndex("dbo.Historiques", new[] { "Personne_Id" });
            DropTable("dbo.Ressources");
            DropTable("dbo.Historiques");
        }
    }
}
