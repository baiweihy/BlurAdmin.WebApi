namespace LegacyApplication.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSomeHRModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "hr.JobPostLevel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(nullable: false, maxLength: 50),
                        UpdateUser = c.String(nullable: false, maxLength: 50),
                        LastAction = c.String(nullable: false, maxLength: 50),
                        Status = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "hr.JobPost",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        PostNature = c.Int(nullable: false),
                        JobPostLevelId = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(nullable: false, maxLength: 50),
                        UpdateUser = c.String(nullable: false, maxLength: 50),
                        LastAction = c.String(nullable: false, maxLength: 50),
                        Status = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("hr.JobPostLevel", t => t.JobPostLevelId)
                .Index(t => t.JobPostLevelId);
            
            AddColumn("hr.Employee", "EmployeeNature", c => c.Int(nullable: false));
            AddColumn("hr.Employee", "EducationNature", c => c.Int(nullable: false));
            AddColumn("hr.Employee", "EducationDegree", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("hr.JobPost", "JobPostLevelId", "hr.JobPostLevel");
            DropIndex("hr.JobPost", new[] { "JobPostLevelId" });
            DropColumn("hr.Employee", "EducationDegree");
            DropColumn("hr.Employee", "EducationNature");
            DropColumn("hr.Employee", "EmployeeNature");
            DropTable("hr.JobPost");
            DropTable("hr.JobPostLevel");
        }
    }
}
