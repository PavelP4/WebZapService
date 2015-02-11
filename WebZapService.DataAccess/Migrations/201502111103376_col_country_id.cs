namespace WebZapService.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class col_country_id : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subscribes", "Country_Id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subscribes", "Country_Id");
        }
    }
}
