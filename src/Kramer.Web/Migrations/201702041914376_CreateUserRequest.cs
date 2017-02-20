namespace Kramer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateUserRequest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Role = c.String(),
                        Username = c.String(),
                        Pending = c.Boolean(nullable: false),
                        RequestedBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.RequestedBy_Id)
                .Index(t => t.RequestedBy_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRequests", "RequestedBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.UserRequests", new[] { "RequestedBy_Id" });
            DropTable("dbo.UserRequests");
        }
    }
}
