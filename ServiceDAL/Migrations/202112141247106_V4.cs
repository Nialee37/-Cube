namespace ServiceDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Personnes", "IsConnected", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Personnes", "IsConnected");
        }
    }
}
