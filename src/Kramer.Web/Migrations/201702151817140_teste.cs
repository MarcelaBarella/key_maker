namespace Kramer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teste : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserRequests", "Name");
            DropColumn("dbo.UserRequests", "Username");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserRequests", "Username", c => c.String());
            AddColumn("dbo.UserRequests", "Name", c => c.String());
        }
    }
}
