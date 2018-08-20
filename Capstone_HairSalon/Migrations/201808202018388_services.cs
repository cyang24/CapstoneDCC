namespace Capstone_HairSalon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class services : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "ServiceId", c => c.Int());
            CreateIndex("dbo.Appointments", "ServiceId");
            AddForeignKey("dbo.Appointments", "ServiceId", "dbo.Services", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "ServiceId", "dbo.Services");
            DropIndex("dbo.Appointments", new[] { "ServiceId" });
            DropColumn("dbo.Appointments", "ServiceId");
        }
    }
}
