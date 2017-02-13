namespace Kramer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateToUserRequests : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserRequests", "RequestDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserRequests", "RequestDate");
        }
    }
}
