namespace Kramer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKeyToUserRequest : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserRequests", "SaleType_Id", "dbo.SaleTypes");
            DropIndex("dbo.UserRequests", new[] { "SaleType_Id" });
            RenameColumn(table: "dbo.UserRequests", name: "SaleType_Id", newName: "SaleTypeId");
            AlterColumn("dbo.UserRequests", "SaleTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.UserRequests", "SaleTypeId");
            AddForeignKey("dbo.UserRequests", "SaleTypeId", "dbo.SaleTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRequests", "SaleTypeId", "dbo.SaleTypes");
            DropIndex("dbo.UserRequests", new[] { "SaleTypeId" });
            AlterColumn("dbo.UserRequests", "SaleTypeId", c => c.Int());
            RenameColumn(table: "dbo.UserRequests", name: "SaleTypeId", newName: "SaleType_Id");
            CreateIndex("dbo.UserRequests", "SaleType_Id");
            AddForeignKey("dbo.UserRequests", "SaleType_Id", "dbo.SaleTypes", "Id");
        }
    }
}
