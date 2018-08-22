namespace Capstone_HairSalon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updaeted : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ServiceTotals", "CheckoutId", "dbo.Checkouts");
            DropIndex("dbo.ServiceTotals", new[] { "CheckoutId" });
            AlterColumn("dbo.ServiceTotals", "CheckoutId", c => c.Int());
            CreateIndex("dbo.ServiceTotals", "CheckoutId");
            AddForeignKey("dbo.ServiceTotals", "CheckoutId", "dbo.Checkouts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServiceTotals", "CheckoutId", "dbo.Checkouts");
            DropIndex("dbo.ServiceTotals", new[] { "CheckoutId" });
            AlterColumn("dbo.ServiceTotals", "CheckoutId", c => c.Int(nullable: false));
            CreateIndex("dbo.ServiceTotals", "CheckoutId");
            AddForeignKey("dbo.ServiceTotals", "CheckoutId", "dbo.Checkouts", "Id", cascadeDelete: true);
        }
    }
}
