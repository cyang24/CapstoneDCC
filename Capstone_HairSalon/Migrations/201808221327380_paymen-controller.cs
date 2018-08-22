namespace Capstone_HairSalon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class paymencontroller : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Payments", "ServiceId", "dbo.Services");
            DropIndex("dbo.Payments", new[] { "ServiceId" });
            DropTable("dbo.Payments");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ServiceId = c.Int(),
                        AdditionalFees = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Payments", "ServiceId");
            AddForeignKey("dbo.Payments", "ServiceId", "dbo.Services", "Id");
        }
    }
}
