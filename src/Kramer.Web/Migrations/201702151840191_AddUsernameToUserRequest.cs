namespace Kramer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUsernameToUserRequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserRequests", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserRequests", "Name");
        }
    }
}
