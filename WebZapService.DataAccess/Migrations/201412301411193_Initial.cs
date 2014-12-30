namespace WebZapService.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Subscribes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Account_Name = c.String(nullable: false, maxLength: 100),
                        Subscription_URL = c.String(nullable: false, maxLength: 4000),
                        Target_URL = c.String(nullable: false, maxLength: 4000),
                        Event = c.String(nullable: false, maxLength: 100),
                        Created = c.DateTime(nullable: false),
                        IsUnsubscribed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Subscribes");
            DropTable("dbo.Devices");
        }
    }
}
