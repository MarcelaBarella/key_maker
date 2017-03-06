namespace Kramer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAdminToAspNetusers : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO AspNetUsers (Email, UserName, Name ) VALUES ('admin@foster.com.br', admin@foster.com.br', 'AdminFoster')");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM AspNetUsers WHERE Name='Admin' ");
        }
    }
}
