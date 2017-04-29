namespace LegacyApplication.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUniqueIndexesToEmployee : DbMigration
    {
        public override void Up()
        {
            CreateIndex("hr.Employee", "No", unique: true);
            CreateIndex("hr.Employee", "UserName", unique: true);
            CreateIndex("hr.Employee", "IdNumber", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("hr.Employee", new[] { "IdNumber" });
            DropIndex("hr.Employee", new[] { "UserName" });
            DropIndex("hr.Employee", new[] { "No" });
        }
    }
}
