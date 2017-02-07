namespace Kramer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBasicSaleTypes : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO SaleTypes VALUES ('BANCO X'), ('BANCO W'), ('BANCO Y'), ('BANCO Z')");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM SaleTypes WHERE Name='BANCO X' OR Name='BANCO W' OR Name='BANCO Z' OR Name='BANCO Y' ");
        }
    }
}
