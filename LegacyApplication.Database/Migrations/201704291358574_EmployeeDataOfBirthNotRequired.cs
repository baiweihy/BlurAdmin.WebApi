namespace LegacyApplication.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeDataOfBirthNotRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("hr.Employee", "DateOfBirth", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("hr.Employee", "DateOfBirth", c => c.DateTime(nullable: false));
        }
    }
}
