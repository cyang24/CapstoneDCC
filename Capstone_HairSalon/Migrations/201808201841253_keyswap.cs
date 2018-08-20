namespace Capstone_HairSalon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class keyswap : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "Accept_Terms", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appointments", "Accept_Terms");
        }
    }
}
