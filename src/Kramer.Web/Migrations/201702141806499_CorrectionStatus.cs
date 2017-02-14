namespace Kramer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrectionStatus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.UserRequests", "StatusId", c => c.Int(nullable: false));
            CreateIndex("dbo.UserRequests", "StatusId");
            AddForeignKey("dbo.UserRequests", "StatusId", "dbo.Status", "Id", cascadeDelete: true);
            DropColumn("dbo.UserRequests", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserRequests", "Status", c => c.Int(nullable: false));
            DropForeignKey("dbo.UserRequests", "StatusId", "dbo.Status");
            DropIndex("dbo.UserRequests", new[] { "StatusId" });
            DropColumn("dbo.UserRequests", "StatusId");
            DropTable("dbo.Status");
        }
    }
}
