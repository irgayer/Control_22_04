namespace Control_22_04.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Journals",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        PublicationDate = c.DateTime(nullable: false),
                        PageCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsDelivered = c.Boolean(nullable: false),
                        Journal_Id = c.Guid(),
                        User_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Journals", t => t.Journal_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Journal_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Login = c.String(),
                        Password = c.String(),
                        IsSubscriber = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Orders", "Journal_Id", "dbo.Journals");
            DropIndex("dbo.Orders", new[] { "User_Id" });
            DropIndex("dbo.Orders", new[] { "Journal_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Orders");
            DropTable("dbo.Journals");
        }
    }
}
