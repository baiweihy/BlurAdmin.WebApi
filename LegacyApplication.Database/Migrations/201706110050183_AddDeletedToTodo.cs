namespace LegacyApplication.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeletedToTodo : DbMigration
    {
        public override void Up()
        {
            AddColumn("work.Todo", "Deleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("work.Todo", "Deleted");
        }
    }
}
