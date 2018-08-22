namespace Capstone_HairSalon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class paymentRedo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Total = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Payments", new[] { "UserId" });
            DropTable("dbo.Payments");
        }
    }
}
