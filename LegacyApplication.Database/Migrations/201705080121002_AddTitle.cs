namespace LegacyApplication.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTitle : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "hr.TitleLevel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(nullable: false, maxLength: 50),
                        UpdateUser = c.String(nullable: false, maxLength: 50),
                        LastAction = c.String(nullable: false, maxLength: 50),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("hr.TitleLevel", t => t.TitleLevelId)
                .Index(t => t.Name, unique: true)
                .Index(t => t.TitleLevelId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("hr.TitlePost", "TitleLevelId", "hr.TitleLevel");
            DropIndex("hr.TitlePost", new[] { "TitleLevelId" });
            DropIndex("hr.TitlePost", new[] { "Name" });
            DropIndex("hr.TitleLevel", new[] { "Name" });
            DropTable("hr.TitlePost");
            DropTable("hr.TitleLevel");
        }
    }
}
