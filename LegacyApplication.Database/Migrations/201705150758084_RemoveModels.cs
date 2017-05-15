namespace LegacyApplication.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("hr.AdministrativePost", "AdministrativeLevelId", "hr.AdministrativeLevel");
            DropForeignKey("hr.TitlePost", "TitleLevelId", "hr.TitleLevel");
            DropIndex("hr.AdministrativePost", new[] { "Name" });
            DropIndex("hr.AdministrativePost", new[] { "AdministrativeLevelId" });
            DropIndex("hr.TitlePost", new[] { "Name" });
            DropIndex("hr.TitlePost", new[] { "TitleLevelId" });
            DropTable("hr.AdministrativePost");
            DropTable("hr.TitlePost");
        }
        
        public override void Down()
        {
            CreateTable(
                "hr.TitlePost",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        TitlePostNature = c.Int(nullable: false),
                        TitleLevelId = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(nullable: false, maxLength: 50),
                        UpdateUser = c.String(nullable: false, maxLength: 50),
                        LastAction = c.String(nullable: false, maxLength: 50),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "hr.AdministrativePost",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        AdministrativePostNature = c.Int(nullable: false),
                        AdministrativeLevelId = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(nullable: false, maxLength: 50),
                        UpdateUser = c.String(nullable: false, maxLength: 50),
                        LastAction = c.String(nullable: false, maxLength: 50),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("hr.TitlePost", "TitleLevelId");
            CreateIndex("hr.TitlePost", "Name", unique: true);
            CreateIndex("hr.AdministrativePost", "AdministrativeLevelId");
            CreateIndex("hr.AdministrativePost", "Name", unique: true);
            AddForeignKey("hr.TitlePost", "TitleLevelId", "hr.TitleLevel", "Id");
            AddForeignKey("hr.AdministrativePost", "AdministrativeLevelId", "hr.AdministrativeLevel", "Id");
        }
    }
}
