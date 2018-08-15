namespace Capstone_HairSalon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appointmentview : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "AppointmentId", c => c.Int());
            CreateIndex("dbo.Customers", "AppointmentId");
            AddForeignKey("dbo.Customers", "AppointmentId", "dbo.Appointments", "Id");
            DropColumn("dbo.Appointments", "Timezone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appointments", "Timezone", c => c.String(nullable: false));
            DropForeignKey("dbo.Customers", "AppointmentId", "dbo.Appointments");
            DropIndex("dbo.Customers", new[] { "AppointmentId" });
            DropColumn("dbo.Customers", "AppointmentId");
        }
    }
}
