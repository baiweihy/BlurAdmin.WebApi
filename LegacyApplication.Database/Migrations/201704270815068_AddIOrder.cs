namespace LegacyApplication.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Department", "Order", c => c.Int(nullable: false));
            AddColumn("dbo.UploadedFile", "Order", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UploadedFile", "Order");
            DropColumn("dbo.Department", "Order");
        }
    }
}
