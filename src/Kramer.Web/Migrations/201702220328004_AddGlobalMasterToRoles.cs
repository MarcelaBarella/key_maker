namespace Kramer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGlobalMasterToRoles : DbMigration
    {
        public override void Up()
        {
            Sql("insert into AspNetRoles values ('-1', 'Global Master')");
        }
        
        public override void Down()
        {
            Sql("delete from AspNetRoles where key = 'GLOBAL MASTER'");
        }
    }
}
