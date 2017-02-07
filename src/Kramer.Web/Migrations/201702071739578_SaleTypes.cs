namespace Kramer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SaleTypes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SaleTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRequestSaleTypes",
                c => new
                    {
                        UserRequest_Id = c.Int(nullable: false),
                        SaleType_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserRequest_Id, t.SaleType_Id })
                .ForeignKey("dbo.UserRequests", t => t.UserRequest_Id, cascadeDelete: true)
                .ForeignKey("dbo.SaleTypes", t => t.SaleType_Id, cascadeDelete: true)
                .Index(t => t.UserRequest_Id)
                .Index(t => t.SaleType_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRequestSaleTypes", "SaleType_Id", "dbo.SaleTypes");
            DropForeignKey("dbo.UserRequestSaleTypes", "UserRequest_Id", "dbo.UserRequests");
            DropIndex("dbo.UserRequestSaleTypes", new[] { "SaleType_Id" });
            DropIndex("dbo.UserRequestSaleTypes", new[] { "UserRequest_Id" });
            DropTable("dbo.UserRequestSaleTypes");
            DropTable("dbo.SaleTypes");
        }
    }
}
