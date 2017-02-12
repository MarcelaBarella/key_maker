namespace Kramer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateProcedureToAddRoleToUser : DbMigration
    {
        public override void Up()
        {
            Sql("CREATE PROCEDURE AddRoleToUser" + Environment.NewLine +
                    "@Email nvarchar(MAX), " + Environment.NewLine +
                    "@RoleName nvarchar(MAX) " + Environment.NewLine +
                    "AS " + Environment.NewLine +
                "BEGIN " + Environment.NewLine +
                "DECLARE @RoleId nvarchar(MAX) " + Environment.NewLine +
                "DECLARE @UserId nvarchar(MAX) " + Environment.NewLine +

                "Select @UserId = Id from AspNetUsers where Email = @Email " + Environment.NewLine +
                "Select @RoleId = Id from AspNetRoles where Name = @RoleName " + Environment.NewLine +

                "INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES (@UserId, @RoleId) " + Environment.NewLine +
                "END");
        }
        
        public override void Down()
        {
            Sql("Drop procedure AddRoleToUser");
        }
    }
}
