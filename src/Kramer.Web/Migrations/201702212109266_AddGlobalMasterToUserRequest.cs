namespace Kramer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGlobalMasterToUserRequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserRequests", "GlobalMaster", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserRequests", "GlobalMaster");
        }
    }
}
