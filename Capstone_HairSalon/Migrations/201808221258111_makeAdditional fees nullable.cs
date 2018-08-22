namespace Capstone_HairSalon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makeAdditionalfeesnullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Payments", "AdditionalFees", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Payments", "AdditionalFees", c => c.Int(nullable: false));
        }
    }
}
