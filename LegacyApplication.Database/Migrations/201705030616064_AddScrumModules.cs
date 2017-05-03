namespace LegacyApplication.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddScrumModules : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Scrum.Bug",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FeatureId = c.Int(nullable: false),
                        SprintId = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 20),
                        Principal = c.String(nullable: false, maxLength: 100),
                        ReproSteps = c.String(nullable: false),
                        SystemInfo = c.String(nullable: false, maxLength: 100),
                        AcceptanceCriteria = c.String(),
                        Priority = c.Int(nullable: false),
                        Severity = c.Int(nullable: false),
                        FinishDate = c.DateTime(),
                        Status = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(nullable: false, maxLength: 50),
                        UpdateUser = c.String(nullable: false, maxLength: 50),
                        LastAction = c.String(nullable: false, maxLength: 50),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Scrum.Feature", t => t.FeatureId)
                .ForeignKey("Scrum.Sprint", t => t.SprintId)
                .Index(t => t.FeatureId)
                .Index(t => t.SprintId);
            
            CreateTable(
                "Scrum.BugTask",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BugId = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 20),
                        Description = c.String(nullable: false),
                        Priority = c.Int(nullable: false),
                        FinishDate = c.DateTime(),
                        Status = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(nullable: false, maxLength: 50),
                        UpdateUser = c.String(nullable: false, maxLength: 50),
                        LastAction = c.String(nullable: false, maxLength: 50),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Scrum.Bug", t => t.BugId)
                .Index(t => t.BugId);
            
            CreateTable(
                "Scrum.Feature",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Principal = c.String(nullable: false, maxLength: 100),
                        TargetDate = c.DateTime(),
                        Priority = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(nullable: false, maxLength: 50),
                        UpdateUser = c.String(nullable: false, maxLength: 50),
                        LastAction = c.String(nullable: false, maxLength: 50),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Scrum.Project", t => t.ProjectId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "Scrum.ProductBacklogItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FeatureId = c.Int(nullable: false),
                        SprintId = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 20),
                        Principal = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false),
                        AcceptanceCriteria = c.String(),
                        Priority = c.Int(nullable: false),
                        FinishDate = c.DateTime(),
                        Status = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(nullable: false, maxLength: 50),
                        UpdateUser = c.String(nullable: false, maxLength: 50),
                        LastAction = c.String(nullable: false, maxLength: 50),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Scrum.Feature", t => t.FeatureId)
                .ForeignKey("Scrum.Sprint", t => t.SprintId)
                .Index(t => t.FeatureId)
                .Index(t => t.SprintId);
            
            CreateTable(
                "Scrum.ProductBacklogItemTask",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductBacklogItemId = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 20),
                        Description = c.String(nullable: false),
                        Priority = c.Int(nullable: false),
                        FinishDate = c.DateTime(),
                        Status = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(nullable: false, maxLength: 50),
                        UpdateUser = c.String(nullable: false, maxLength: 50),
                        LastAction = c.String(nullable: false, maxLength: 50),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Scrum.ProductBacklogItem", t => t.ProductBacklogItemId)
                .Index(t => t.ProductBacklogItemId);
            
            CreateTable(
                "Scrum.Sprint",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(nullable: false, maxLength: 50),
                        UpdateUser = c.String(nullable: false, maxLength: 50),
                        LastAction = c.String(nullable: false, maxLength: 50),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Scrum.Project", t => t.ProjectId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "Scrum.Project",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(maxLength: 4000),
                        ProjectManager = c.String(nullable: false, maxLength: 100),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(nullable: false, maxLength: 50),
                        UpdateUser = c.String(nullable: false, maxLength: 50),
                        LastAction = c.String(nullable: false, maxLength: 50),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Scrum.ProjectTeamMember",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 100),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(nullable: false, maxLength: 50),
                        UpdateUser = c.String(nullable: false, maxLength: 50),
                        LastAction = c.String(nullable: false, maxLength: 50),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Scrum.Project", t => t.ProjectId)
                .Index(t => new { t.ProjectId, t.UserName }, unique: true, name: "IX_Internal_ProjectTeamMembers_ProjectIdAndUserName");
            
            DropColumn("hr.Department", "Status");
            DropColumn("hr.Employee", "Status");
            DropColumn("hr.JobPost", "Status");
            DropColumn("hr.JobPostLevel", "Status");
            DropColumn("dbo.UploadedFile", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UploadedFile", "Status", c => c.Int(nullable: false));
            AddColumn("hr.JobPostLevel", "Status", c => c.Int(nullable: false));
            AddColumn("hr.JobPost", "Status", c => c.Int(nullable: false));
            AddColumn("hr.Employee", "Status", c => c.Int(nullable: false));
            AddColumn("hr.Department", "Status", c => c.Int(nullable: false));
            DropForeignKey("Scrum.Bug", "SprintId", "Scrum.Sprint");
            DropForeignKey("Scrum.Bug", "FeatureId", "Scrum.Feature");
            DropForeignKey("Scrum.Feature", "ProjectId", "Scrum.Project");
            DropForeignKey("Scrum.ProductBacklogItem", "SprintId", "Scrum.Sprint");
            DropForeignKey("Scrum.Sprint", "ProjectId", "Scrum.Project");
            DropForeignKey("Scrum.ProjectTeamMember", "ProjectId", "Scrum.Project");
            DropForeignKey("Scrum.ProductBacklogItemTask", "ProductBacklogItemId", "Scrum.ProductBacklogItem");
            DropForeignKey("Scrum.ProductBacklogItem", "FeatureId", "Scrum.Feature");
            DropForeignKey("Scrum.BugTask", "BugId", "Scrum.Bug");
            DropIndex("Scrum.ProjectTeamMember", "IX_Internal_ProjectTeamMembers_ProjectIdAndUserName");
            DropIndex("Scrum.Sprint", new[] { "ProjectId" });
            DropIndex("Scrum.ProductBacklogItemTask", new[] { "ProductBacklogItemId" });
            DropIndex("Scrum.ProductBacklogItem", new[] { "SprintId" });
            DropIndex("Scrum.ProductBacklogItem", new[] { "FeatureId" });
            DropIndex("Scrum.Feature", new[] { "ProjectId" });
            DropIndex("Scrum.BugTask", new[] { "BugId" });
            DropIndex("Scrum.Bug", new[] { "SprintId" });
            DropIndex("Scrum.Bug", new[] { "FeatureId" });
            DropTable("Scrum.ProjectTeamMember");
            DropTable("Scrum.Project");
            DropTable("Scrum.Sprint");
            DropTable("Scrum.ProductBacklogItemTask");
            DropTable("Scrum.ProductBacklogItem");
            DropTable("Scrum.Feature");
            DropTable("Scrum.BugTask");
            DropTable("Scrum.Bug");
        }
    }
}
