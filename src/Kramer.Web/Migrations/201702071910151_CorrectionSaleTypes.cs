namespace Kramer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrectionSaleTypes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserRequestSaleTypes", "UserRequest_Id", "dbo.UserRequests");
            DropForeignKey("dbo.UserRequestSaleTypes", "SaleType_Id", "dbo.SaleTypes");
            DropIndex("dbo.UserRequestSaleTypes", new[] { "UserRequest_Id" });
            DropIndex("dbo.UserRequestSaleTypes", new[] { "SaleType_Id" });
            AddColumn("dbo.UserRequests", "SaleType_Id", c => c.Int());
            CreateIndex("dbo.UserRequests", "SaleType_Id");
            AddForeignKey("dbo.UserRequests", "SaleType_Id", "dbo.SaleTypes", "Id");
            DropTable("dbo.UserRequestSaleTypes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserRequestSaleTypes",
                c => new
                    {
                        UserRequest_Id = c.Int(nullable: false),
                        SaleType_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserRequest_Id, t.SaleType_Id });
            
            DropForeignKey("dbo.UserRequests", "SaleType_Id", "dbo.SaleTypes");
            DropIndex("dbo.UserRequests", new[] { "SaleType_Id" });
            DropColumn("dbo.UserRequests", "SaleType_Id");
            CreateIndex("dbo.UserRequestSaleTypes", "SaleType_Id");
            CreateIndex("dbo.UserRequestSaleTypes", "UserRequest_Id");
            AddForeignKey("dbo.UserRequestSaleTypes", "SaleType_Id", "dbo.SaleTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserRequestSaleTypes", "UserRequest_Id", "dbo.UserRequests", "Id", cascadeDelete: true);
        }
    }
}
