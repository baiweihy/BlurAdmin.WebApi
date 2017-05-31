namespace LegacyApplication.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveAllowance : DbMigration
    {
        public override void Up()
        {
            DropIndex("hr.AllowanceLevel", new[] { "Name" });
            DropTable("hr.AllowanceLevel");
        }
        
        public override void Down()
        {
            CreateTable(
                "hr.AllowanceLevel",
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("hr.AllowanceLevel", "Name", unique: true);
        }
    }
}
