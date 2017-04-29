namespace LegacyApplication.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmployee : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "hr.Employee",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        No = c.String(maxLength: 50),
                        UserName = c.String(maxLength: 50),
                        EmployeeStatus = c.Int(nullable: false),
                        DepartmentId = c.Int(),
                        Gender = c.Int(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        IdNumber = c.String(maxLength: 50),
                        PhoneNumber = c.String(maxLength: 50),
                        MaritalStatus = c.Int(nullable: false),
                        PoliticalStatus = c.Int(nullable: false),
                        EmailAddress = c.String(maxLength: 50),
                        Remark = c.String(maxLength: 500),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(nullable: false, maxLength: 50),
                        UpdateUser = c.String(nullable: false, maxLength: 50),
                        LastAction = c.String(nullable: false, maxLength: 50),
                        Status = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("hr.Department", t => t.DepartmentId)
                .Index(t => t.DepartmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("hr.Employee", "DepartmentId", "hr.Department");
            DropIndex("hr.Employee", new[] { "DepartmentId" });
            DropTable("hr.Employee");
        }
    }
}
