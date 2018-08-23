namespace Capstone_HairSalon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eventsadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EventText = c.String(),
                        Start_date = c.DateTime(nullable: false),
                        End_date = c.DateTime(nullable: false),
                        StylistId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stylists", t => t.StylistId)
                .Index(t => t.StylistId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "StylistId", "dbo.Stylists");
            DropIndex("dbo.Events", new[] { "StylistId" });
            DropTable("dbo.Events");
        }
    }
}
