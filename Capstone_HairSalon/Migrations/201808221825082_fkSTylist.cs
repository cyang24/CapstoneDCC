namespace Capstone_HairSalon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fkSTylist : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stylists", "CheckoutId", c => c.Int());
            CreateIndex("dbo.Stylists", "CheckoutId");
            AddForeignKey("dbo.Stylists", "CheckoutId", "dbo.Checkouts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stylists", "CheckoutId", "dbo.Checkouts");
            DropIndex("dbo.Stylists", new[] { "CheckoutId" });
            DropColumn("dbo.Stylists", "CheckoutId");
        }
    }
}
