namespace LegacyApplication.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveScrum : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Scrum.BugTask", "BugId", "Scrum.Bug");
            DropForeignKey("Scrum.ProductBacklogItem", "FeatureId", "Scrum.Feature");
            DropForeignKey("Scrum.ProductBacklogItemTask", "ProductBacklogItemId", "Scrum.ProductBacklogItem");
            DropForeignKey("Scrum.ProjectTeamMember", "ProjectId", "Scrum.Project");
            DropForeignKey("Scrum.Sprint", "ProjectId", "Scrum.Project");
            DropForeignKey("Scrum.ProductBacklogItem", "SprintId", "Scrum.Sprint");
            DropForeignKey("Scrum.Feature", "ProjectId", "Scrum.Project");
            DropForeignKey("Scrum.Bug", "FeatureId", "Scrum.Feature");
            DropForeignKey("Scrum.Bug", "SprintId", "Scrum.Sprint");
            DropIndex("Scrum.Bug", new[] { "FeatureId" });
            DropIndex("Scrum.Bug", new[] { "SprintId" });
            DropIndex("Scrum.BugTask", new[] { "BugId" });
            DropIndex("Scrum.Feature", new[] { "ProjectId" });
            DropIndex("Scrum.ProductBacklogItem", new[] { "FeatureId" });
            DropIndex("Scrum.ProductBacklogItem", new[] { "SprintId" });
            DropIndex("Scrum.ProductBacklogItemTask", new[] { "ProductBacklogItemId" });
            DropIndex("Scrum.Sprint", new[] { "ProjectId" });
            DropIndex("Scrum.ProjectTeamMember", "IX_Internal_ProjectTeamMembers_ProjectIdAndUserName");
            DropTable("Scrum.Bug");
            DropTable("Scrum.BugTask");
            DropTable("Scrum.Feature");
            DropTable("Scrum.ProductBacklogItem");
            DropTable("Scrum.ProductBacklogItemTask");
            DropTable("Scrum.Sprint");
            DropTable("Scrum.Project");
            DropTable("Scrum.ProjectTeamMember");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("Scrum.ProjectTeamMember", new[] { "ProjectId", "UserName" }, unique: true, name: "IX_Internal_ProjectTeamMembers_ProjectIdAndUserName");
            CreateIndex("Scrum.Sprint", "ProjectId");
            CreateIndex("Scrum.ProductBacklogItemTask", "ProductBacklogItemId");
            CreateIndex("Scrum.ProductBacklogItem", "SprintId");
            CreateIndex("Scrum.ProductBacklogItem", "FeatureId");
            CreateIndex("Scrum.Feature", "ProjectId");
            CreateIndex("Scrum.BugTask", "BugId");
            CreateIndex("Scrum.Bug", "SprintId");
            CreateIndex("Scrum.Bug", "FeatureId");
            AddForeignKey("Scrum.Bug", "SprintId", "Scrum.Sprint", "Id");
            AddForeignKey("Scrum.Bug", "FeatureId", "Scrum.Feature", "Id");
            AddForeignKey("Scrum.Feature", "ProjectId", "Scrum.Project", "Id");
            AddForeignKey("Scrum.ProductBacklogItem", "SprintId", "Scrum.Sprint", "Id");
            AddForeignKey("Scrum.Sprint", "ProjectId", "Scrum.Project", "Id");
            AddForeignKey("Scrum.ProjectTeamMember", "ProjectId", "Scrum.Project", "Id");
            AddForeignKey("Scrum.ProductBacklogItemTask", "ProductBacklogItemId", "Scrum.ProductBacklogItem", "Id");
            AddForeignKey("Scrum.ProductBacklogItem", "FeatureId", "Scrum.Feature", "Id");
            AddForeignKey("Scrum.BugTask", "BugId", "Scrum.Bug", "Id");
        }
    }
}
