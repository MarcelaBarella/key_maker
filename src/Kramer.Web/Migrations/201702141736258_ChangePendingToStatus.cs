namespace Kramer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePendingToStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserRequests", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.UserRequests", "Pending");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserRequests", "Pending", c => c.Boolean(nullable: false));
            DropColumn("dbo.UserRequests", "Status");
        }
    }
}
