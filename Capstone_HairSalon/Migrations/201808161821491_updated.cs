namespace Capstone_HairSalon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "Date", c => c.String(nullable: false));
            DropColumn("dbo.Appointments", "Time");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appointments", "Time", c => c.String(nullable: false));
            DropColumn("dbo.Appointments", "Date");
        }
    }
}
