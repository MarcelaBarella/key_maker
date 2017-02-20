namespace Kramer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrecaoUserRequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserRequests", "Usershare", c => c.String());
            DropColumn("dbo.UserRequests", "Username");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserRequests", "Username", c => c.String());
            DropColumn("dbo.UserRequests", "Usershare");
        }
    }
}
