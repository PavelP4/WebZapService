namespace WebZapService.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class col_phone : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subscribes", "Phone", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subscribes", "Phone");
        }
    }
}
