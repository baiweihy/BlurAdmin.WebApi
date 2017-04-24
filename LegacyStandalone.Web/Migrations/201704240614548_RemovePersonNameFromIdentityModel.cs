namespace LegacyStandalone.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovePersonNameFromIdentityModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "PersonName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "PersonName", c => c.String());
        }
    }
}
