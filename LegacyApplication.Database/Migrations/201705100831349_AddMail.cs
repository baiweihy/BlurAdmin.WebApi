namespace LegacyApplication.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "work.InternalMailAttachment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MailId = c.Int(nullable: false),
                        FileId = c.Int(nullable: false),
                        FileName = c.String(nullable: false, maxLength: 200),
                        Path = c.String(nullable: false, maxLength: 200),
                        Size = c.Long(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(nullable: false, maxLength: 50),
                        UpdateUser = c.String(nullable: false, maxLength: 50),
                        LastAction = c.String(nullable: false, maxLength: 50),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("work.InternalMail", t => t.MailId)
                .Index(t => t.MailId);
            
            CreateTable(
                "work.InternalMail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 50),
                        PersonName = c.String(maxLength: 100),
                        Title = c.String(nullable: false, maxLength: 200),
                        Body = c.String(),
                        SendTime = c.DateTime(nullable: false),
                        HasDeleted = c.Boolean(nullable: false),
                        MailType = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(),
                        UpdateUser = c.String(),
                        LastAction = c.String(),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "work.InternalMailTo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MailId = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 50),
                        PersonName = c.String(maxLength: 100),
                        HasRead = c.Boolean(nullable: false),
                        ReadTime = c.DateTime(),
                        HasDeleted = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(nullable: false, maxLength: 50),
                        UpdateUser = c.String(nullable: false, maxLength: 50),
                        LastAction = c.String(nullable: false, maxLength: 50),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("work.InternalMail", t => t.MailId)
                .Index(t => t.MailId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("work.InternalMailAttachment", "MailId", "work.InternalMail");
            DropForeignKey("work.InternalMailTo", "MailId", "work.InternalMail");
            DropIndex("work.InternalMailTo", new[] { "MailId" });
            DropIndex("work.InternalMailAttachment", new[] { "MailId" });
            DropTable("work.InternalMailTo");
            DropTable("work.InternalMail");
            DropTable("work.InternalMailAttachment");
        }
    }
}
