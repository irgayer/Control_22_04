namespace Control_22_04.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FinalMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subscriptions", "SubscriptionVersion", c => c.Int(nullable: false));
            DropColumn("dbo.Subscriptions", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Subscriptions", "Price", c => c.Int(nullable: false));
            DropColumn("dbo.Subscriptions", "SubscriptionVersion");
        }
    }
}
