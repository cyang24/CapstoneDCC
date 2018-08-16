namespace Capstone_HairSalon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notRequired : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "StylistId", c => c.Int());
            CreateIndex("dbo.Appointments", "StylistId");
            AddForeignKey("dbo.Appointments", "StylistId", "dbo.Stylists", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "StylistId", "dbo.Stylists");
            DropIndex("dbo.Appointments", new[] { "StylistId" });
            DropColumn("dbo.Appointments", "StylistId");
        }
    }
}
