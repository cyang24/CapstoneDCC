namespace Capstone_HairSalon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class udatedForum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "FirstName", c => c.String(nullable: false));
            AddColumn("dbo.Appointments", "LastName", c => c.String(nullable: false));
            DropColumn("dbo.Appointments", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appointments", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Appointments", "LastName");
            DropColumn("dbo.Appointments", "FirstName");
        }
    }
}
