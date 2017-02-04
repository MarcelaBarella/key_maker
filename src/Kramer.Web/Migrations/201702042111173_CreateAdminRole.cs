namespace Kramer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAdminRole : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO AspNetRoles VALUES (1, 'Admin')");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM AspNetRoles WHERE Name='Admin' ");
        }
    }
}
