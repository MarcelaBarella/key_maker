namespace Kramer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKeyToUserRequest : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserRequests", "SaleType_Id");
            DropForeignKey("dbo.UserRequests", "SaleTypeId", "dbo.SaleTypes");
            DropIndex("dbo.UserRequests", new[] { "SaleTypeId" });
            AddColumn("dbo.UserRequests", "SaleTypeId", c => c.Int());
            CreateIndex("dbo.UserRequests", "SaleTypeId");
            AddForeignKey("dbo.UserRequests", "SaleTypeId", "dbo.SaleTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRequests", "SaleTypeId", "dbo.SaleTypes");
            DropIndex("dbo.UserRequests", new[] { "SaleTypeId" });
            AddColumn("dbo.UserRequests", "SaleType_Id", c => c.Int(nullable: false));
            DropColumn("dbo.UserRequests", "SaleTypeId");
            CreateIndex("dbo.UserRequests", "SaleTypeId");
            AddForeignKey("dbo.UserRequests", "SaleTypeId", "dbo.SaleTypes", "Id", cascadeDelete: true);
        }
    }
}
