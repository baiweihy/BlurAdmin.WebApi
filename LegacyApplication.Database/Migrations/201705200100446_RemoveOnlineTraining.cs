namespace LegacyApplication.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveOnlineTraining : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("ot.Category", "ParentId", "ot.Category");
            DropIndex("ot.Category", new[] { "ParentId" });
            DropTable("ot.Category");
        }
        
        public override void Down()
        {
            CreateTable(
                "ot.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Description = c.String(nullable: false, maxLength: 500),
                        ParentId = c.Int(),
                        AncestorIds = c.String(maxLength: 200),
                        IsAbstract = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(nullable: false, maxLength: 50),
                        UpdateUser = c.String(nullable: false, maxLength: 50),
                        LastAction = c.String(nullable: false, maxLength: 50),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("ot.Category", "ParentId");
            AddForeignKey("ot.Category", "ParentId", "ot.Category", "Id");
        }
    }
}
