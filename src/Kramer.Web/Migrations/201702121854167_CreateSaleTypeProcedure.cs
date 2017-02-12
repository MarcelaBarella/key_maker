namespace Kramer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateSaleTypeProcedure : DbMigration
    {
        public override void Up()
        {
            Sql("CREATE PROCEDURE AddSaleType" + Environment.NewLine +
                "@Nome nvarchar(MAX)" + Environment.NewLine +
                "AS" + Environment.NewLine +
                "BEGIN" + Environment.NewLine +
                    "DECLARE @NewRoleId integer" + Environment.NewLine +
                    "Select @NewRoleId = MAX(ID) + 1 from AspNetRoles" + Environment.NewLine +

                    "INSERT INTO SaleTypes (Name) values (@Nome)" + Environment.NewLine +
                    "INSERT INTO AspNetRoles values(@NewRoleId, @Nome)" + Environment.NewLine +
                "END");
        }
        
        public override void Down()
        {
            Sql("drop procedure AddSaleType");
        }
    }
}
