namespace LegacyApplication.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEducationalBackground : DbMigration
    {
        public override void Up()
        {
            AddColumn("hr.Employee", "EducationalBackground", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("hr.Employee", "EducationalBackground");
        }
    }
}
