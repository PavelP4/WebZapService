namespace WebZapService.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class col_api_key : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subscribes", "API_Key", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subscribes", "API_Key");
        }
    }
}
