namespace Kramer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TesteDeCorrecaoDeErros : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.UserRequests", name: "SaleType_Id", newName: "SaleTypeId");
            RenameIndex(table: "dbo.UserRequests", name: "IX_SaleType_Id", newName: "IX_SaleTypeId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.UserRequests", name: "IX_SaleTypeId", newName: "IX_SaleType_Id");
            RenameColumn(table: "dbo.UserRequests", name: "SaleTypeId", newName: "SaleType_Id");
        }
    }
}
