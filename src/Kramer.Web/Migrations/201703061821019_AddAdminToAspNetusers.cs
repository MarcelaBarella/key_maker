namespace Kramer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAdminToAspNetusers : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO AspNetUsers (Id, Email, EmailConfirmed, PasswordHash, SecurityStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEndDateutc, LockoutEnabled, AccessFailedCount, UserName, Name ) VALUES (NEWID(), 'admin@foster.com.br', 0 , 'AESe7mhuA9ed1I0mmFlVwjjBHdBHsNwPbwkNeBAlfhCBw+5nfd87aqPRKI7E3cJgKA==', '6059668f-03ea-4e00-84b5-f84ae051eb7d', NULL, 0, 0,  NULL, 1, 0, 'admin@foster.com.br', 'AdminFoster')");
        }
        // senha do usuário adm criado pela migration é "administrador"
        public override void Down()
        {
            Sql("DELETE FROM AspNetUsers WHERE Name='Admin' ");
        }
    }
}
