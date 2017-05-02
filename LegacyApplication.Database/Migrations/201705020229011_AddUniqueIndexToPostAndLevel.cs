namespace LegacyApplication.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUniqueIndexToPostAndLevel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("hr.JobPostLevel", "Name", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("hr.JobPostLevel", "Name", unique: true);
            CreateIndex("hr.JobPost", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("hr.JobPost", new[] { "Name" });
            DropIndex("hr.JobPostLevel", new[] { "Name" });
            AlterColumn("hr.JobPostLevel", "Name", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
