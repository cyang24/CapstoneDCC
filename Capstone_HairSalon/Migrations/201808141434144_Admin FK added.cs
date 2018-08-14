namespace Capstone_HairSalon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdminFKadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Admins", "UserId");
            AddForeignKey("dbo.Admins", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Admins", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Admins", new[] { "UserId" });
            DropColumn("dbo.Admins", "UserId");
        }
    }
}
