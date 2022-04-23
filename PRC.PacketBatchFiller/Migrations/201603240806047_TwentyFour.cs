namespace PRC.PacketBatchFiller.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TwentyFour : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IssuesOfSecurities",
                c => new
                    {
                        IssueOfSecuritiesId = c.Long(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Number = c.String(),
                        LegalEntity_UnitId = c.Long(),
                    })
                .PrimaryKey(t => t.IssueOfSecuritiesId)
                .ForeignKey("dbo.LegalEntities", t => t.LegalEntity_UnitId)
                .Index(t => t.LegalEntity_UnitId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IssuesOfSecurities", "LegalEntity_UnitId", "dbo.LegalEntities");
            DropIndex("dbo.IssuesOfSecurities", new[] { "LegalEntity_UnitId" });
            DropTable("dbo.IssuesOfSecurities");
        }
    }
}
