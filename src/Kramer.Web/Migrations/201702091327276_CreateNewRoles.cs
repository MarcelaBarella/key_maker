namespace Kramer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateNewRoles : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO AspNetRoles VALUES (2, 'BANCO X'), (3, 'BANCO Y'), (4, 'BANCO Z')");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM AspNetRoles WHERE Name='BANCO X' OR Name='BANCO Y' OR Name='BANCO Z'");
        }
    }
}
