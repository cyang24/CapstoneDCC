namespace Capstone_HairSalon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class noForKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Stylists", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Customers", new[] { "UserId" });
            DropIndex("dbo.Stylists", new[] { "UserId" });
            DropColumn("dbo.Customers", "UserId");
            DropColumn("dbo.Stylists", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stylists", "UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.Customers", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Stylists", "UserId");
            CreateIndex("dbo.Customers", "UserId");
            AddForeignKey("dbo.Stylists", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Customers", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
