namespace Capstone_HairSalon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class booleanaddcutomers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "ConfirmAppointment", c => c.Boolean(nullable: false));
            AddColumn("dbo.Appointments", "DenyAppointment", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appointments", "DenyAppointment");
            DropColumn("dbo.Appointments", "ConfirmAppointment");
        }
    }
}
