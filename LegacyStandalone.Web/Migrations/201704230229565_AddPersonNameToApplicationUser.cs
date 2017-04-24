namespace LegacyStandalone.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPersonNameToApplicationUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "PersonName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "PersonName");
        }
    }
}
