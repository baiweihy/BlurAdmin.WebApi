namespace LegacyApplication.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHumanResourcesModule : DbMigration
    {
        public override void Up()
        {
            MoveTable(name: "dbo.Department", newSchema: "hr");
        }
        
        public override void Down()
        {
            MoveTable(name: "hr.Department", newSchema: "dbo");
        }
    }
}
