namespace Kramer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePendingToStatus : DbMigration
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

            Sql("insert into Status (Name) values ('PENDENTE'), ('CONCLUIDO'), ('CANCELADO')");
            
            AddColumn("dbo.UserRequests", "StatusId", c => c.Int(nullable: false, defaultValue: 1));
            CreateIndex("dbo.UserRequests", "StatusId");
            AddForeignKey("dbo.UserRequests", "StatusId", "dbo.Status", "Id", cascadeDelete: true);
            DropColumn("dbo.UserRequests", "Pending");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserRequests", "Pending", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.UserRequests", "StatusId", "dbo.Status");
            DropIndex("dbo.UserRequests", new[] { "StatusId" });
            DropColumn("dbo.UserRequests", "StatusId");
            DropTable("dbo.Status");
        }
    }
}
