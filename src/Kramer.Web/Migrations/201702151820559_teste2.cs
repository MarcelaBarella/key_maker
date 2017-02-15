namespace Kramer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teste2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserRequests", "Name", c => c.String());
            AddColumn("dbo.UserRequests", "Username", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserRequests", "Username");
            DropColumn("dbo.UserRequests", "Name");
        }
    }
}
