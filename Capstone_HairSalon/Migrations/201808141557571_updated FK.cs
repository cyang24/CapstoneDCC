namespace Capstone_HairSalon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedFK : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.Stylists", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Customers", "UserId");
            CreateIndex("dbo.Stylists", "UserId");
            AddForeignKey("dbo.Customers", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Stylists", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stylists", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Customers", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Stylists", new[] { "UserId" });
            DropIndex("dbo.Customers", new[] { "UserId" });
            DropColumn("dbo.Stylists", "UserId");
            DropColumn("dbo.Customers", "UserId");
        }
    }
}
