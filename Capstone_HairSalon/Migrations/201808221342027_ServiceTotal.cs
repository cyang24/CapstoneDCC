namespace Capstone_HairSalon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ServiceTotal : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ServiceTotals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ServiceId = c.Int(),
                        Payment_Id = c.Int(),
                        AdditionalFees = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Payments", t => t.Payment_Id)
                .ForeignKey("dbo.Services", t => t.ServiceId)
                .Index(t => t.ServiceId)
                .Index(t => t.Payment_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServiceTotals", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.ServiceTotals", "Payment_Id", "dbo.Payments");
            DropIndex("dbo.ServiceTotals", new[] { "Payment_Id" });
            DropIndex("dbo.ServiceTotals", new[] { "ServiceId" });
            DropTable("dbo.ServiceTotals");
        }
    }
}
