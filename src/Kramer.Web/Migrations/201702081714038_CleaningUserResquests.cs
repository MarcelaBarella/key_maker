namespace Kramer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CleaningUserResquests : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserRequests", "Role");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserRequests", "Role", c => c.String());
        }
    }
}
