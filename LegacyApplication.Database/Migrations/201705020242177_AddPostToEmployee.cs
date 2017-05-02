namespace LegacyApplication.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostToEmployee : DbMigration
    {
        public override void Up()
        {
            AddColumn("hr.Employee", "JobPostId", c => c.Int());
            CreateIndex("hr.Employee", "JobPostId");
            AddForeignKey("hr.Employee", "JobPostId", "hr.JobPost", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("hr.Employee", "JobPostId", "hr.JobPost");
            DropIndex("hr.Employee", new[] { "JobPostId" });
            DropColumn("hr.Employee", "JobPostId");
        }
    }
}
