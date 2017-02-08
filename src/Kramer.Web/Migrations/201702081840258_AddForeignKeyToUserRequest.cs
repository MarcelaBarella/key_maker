namespace Kramer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKeyToUserRequest : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserRequests", "SaleTypeId", "dbo.SaleTypes");
            DropIndex("dbo.UserRequests", new[] { "SaleTypeId" });
            AlterColumn("dbo.UserRequests", "SaleTypeId", c => c.Int());
            CreateIndex("dbo.UserRequests", "SaleTypeId");
            AddForeignKey("dbo.UserRequests", "SaleTypeId", "dbo.SaleTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRequests", "SaleTypeId", "dbo.SaleTypes");
            DropIndex("dbo.UserRequests", new[] { "SaleTypeId" });
            AlterColumn("dbo.UserRequests", "SaleTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.UserRequests", "SaleTypeId");
            AddForeignKey("dbo.UserRequests", "SaleTypeId", "dbo.SaleTypes", "Id", cascadeDelete: true);
        }
    }
}
