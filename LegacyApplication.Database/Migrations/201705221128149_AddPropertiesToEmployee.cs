namespace LegacyApplication.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPropertiesToEmployee : DbMigration
    {
        public override void Up()
        {
            AddColumn("hr.Employee", "NationalityId", c => c.Int());
            AddColumn("hr.Employee", "Title", c => c.String(maxLength: 100));
            AddColumn("hr.Employee", "TitleLevelId", c => c.Int());
            AddColumn("hr.Employee", "AdministrativeLevelId", c => c.Int());
            CreateIndex("hr.Employee", "NationalityId");
            CreateIndex("hr.Employee", "TitleLevelId");
            CreateIndex("hr.Employee", "AdministrativeLevelId");
            AddForeignKey("hr.Employee", "AdministrativeLevelId", "hr.AdministrativeLevel", "Id");
            AddForeignKey("hr.Employee", "NationalityId", "hr.Nationality", "Id");
            AddForeignKey("hr.Employee", "TitleLevelId", "hr.TitleLevel", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("hr.Employee", "TitleLevelId", "hr.TitleLevel");
            DropForeignKey("hr.Employee", "NationalityId", "hr.Nationality");
            DropForeignKey("hr.Employee", "AdministrativeLevelId", "hr.AdministrativeLevel");
            DropIndex("hr.Employee", new[] { "AdministrativeLevelId" });
            DropIndex("hr.Employee", new[] { "TitleLevelId" });
            DropIndex("hr.Employee", new[] { "NationalityId" });
            DropColumn("hr.Employee", "AdministrativeLevelId");
            DropColumn("hr.Employee", "TitleLevelId");
            DropColumn("hr.Employee", "Title");
            DropColumn("hr.Employee", "NationalityId");
        }
    }
}
