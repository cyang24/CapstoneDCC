namespace Capstone_HairSalon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class noFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Admins", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Admins", new[] { "UserId" });
            DropColumn("dbo.Admins", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Admins", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Admins", "UserId");
            AddForeignKey("dbo.Admins", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
