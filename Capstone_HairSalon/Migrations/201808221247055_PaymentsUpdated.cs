namespace Capstone_HairSalon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentsUpdated : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Payments", "AppointmentId", "dbo.Appointments");
            DropIndex("dbo.Payments", new[] { "AppointmentId" });
            DropColumn("dbo.Payments", "AppointmentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Payments", "AppointmentId", c => c.Int());
            CreateIndex("dbo.Payments", "AppointmentId");
            AddForeignKey("dbo.Payments", "AppointmentId", "dbo.Appointments", "Id");
        }
    }
}
