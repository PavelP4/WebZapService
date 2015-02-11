namespace WebZapService.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class col_device_id : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subscribes", "Device_Id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subscribes", "Device_Id");
        }
    }
}
