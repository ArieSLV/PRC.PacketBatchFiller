namespace PRC.PacketBatchFiller.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Thirteen : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LegalEntity", "Unit_UnitId", "dbo.Units");
            DropIndex("dbo.LegalEntity", new[] { "Unit_UnitId" });
            CreateTable(
                "dbo.ShareholderAccount",
                c => new
                    {
                        ShareholderAccountId = c.Long(nullable: false, identity: true),
                        Number = c.String(),
                        ShareholderAccountType = c.Int(nullable: false),
                        SecuritiesIssuer_UnitId = c.Long(),
                        Unit_UnitId = c.Long(),
                    })
                .PrimaryKey(t => t.ShareholderAccountId)
                .ForeignKey("dbo.LegalEntity", t => t.SecuritiesIssuer_UnitId)
                .ForeignKey("dbo.Units", t => t.Unit_UnitId)
                .Index(t => t.SecuritiesIssuer_UnitId)
                .Index(t => t.Unit_UnitId);
            
            DropColumn("dbo.LegalEntity", "Unit_UnitId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LegalEntity", "Unit_UnitId", c => c.Long());
            DropForeignKey("dbo.ShareholderAccount", "Unit_UnitId", "dbo.Units");
            DropForeignKey("dbo.ShareholderAccount", "SecuritiesIssuer_UnitId", "dbo.LegalEntity");
            DropIndex("dbo.ShareholderAccount", new[] { "Unit_UnitId" });
            DropIndex("dbo.ShareholderAccount", new[] { "SecuritiesIssuer_UnitId" });
            DropTable("dbo.ShareholderAccount");
            CreateIndex("dbo.LegalEntity", "Unit_UnitId");
            AddForeignKey("dbo.LegalEntity", "Unit_UnitId", "dbo.Units", "UnitId");
        }
    }
}
