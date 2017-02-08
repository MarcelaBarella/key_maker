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
            AlterColumn("dbo.UserRequests", "SaleTypeId", c => c.Int());
            CreateIndex("dbo.UserRequests", "SaleTypeId");
            AddForeignKey("dbo.UserRequests", "SaleTypeId", "dbo.SaleTypes", "Id", cascadeDelete: true);

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
