namespace LegacyApplication.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUserNameFromEmployee : DbMigration
    {
        public override void Up()
        {
            DropIndex("hr.Employee", new[] { "No" });
            DropIndex("hr.Employee", new[] { "UserName" });
            AlterColumn("hr.Employee", "No", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("hr.Employee", "No", unique: true);
            DropColumn("hr.Employee", "UserName");
        }
        
        public override void Down()
        {
            AddColumn("hr.Employee", "UserName", c => c.String(maxLength: 50));
            DropIndex("hr.Employee", new[] { "No" });
            AlterColumn("hr.Employee", "No", c => c.String(maxLength: 50));
            CreateIndex("hr.Employee", "UserName", unique: true);
            CreateIndex("hr.Employee", "No", unique: true);
        }
    }
}
