namespace Capstone_HairSalon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appointmentdated : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Appointments", "CreatedAt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appointments", "CreatedAt", c => c.DateTime(nullable: false));
        }
    }
}
