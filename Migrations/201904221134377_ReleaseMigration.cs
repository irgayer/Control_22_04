namespace Control_22_04.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReleaseMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Subscriptions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Price = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Users", "Subscription_Id", c => c.Guid());
            CreateIndex("dbo.Users", "Subscription_Id");
            AddForeignKey("dbo.Users", "Subscription_Id", "dbo.Subscriptions", "Id");
            DropColumn("dbo.Users", "IsSubscriber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "IsSubscriber", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Users", "Subscription_Id", "dbo.Subscriptions");
            DropIndex("dbo.Users", new[] { "Subscription_Id" });
            DropColumn("dbo.Users", "Subscription_Id");
            DropTable("dbo.Subscriptions");
        }
    }
}
