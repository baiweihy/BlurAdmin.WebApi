namespace LegacyStandalone.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String(maxLength: 50));
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String(maxLength: 50));
            AddColumn("dbo.AspNetUsers", "Occupation", c => c.String(maxLength: 100));
            AddColumn("dbo.AspNetUsers", "PictureFileId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "PictureFileId");
            DropColumn("dbo.AspNetUsers", "Occupation");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
        }
    }
}
