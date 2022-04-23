namespace PRC.PacketBatchFiller.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Twelve : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Units", "Unit_UnitId", c => c.Long());
            AddColumn("dbo.LegalEntity", "Unit_UnitId", c => c.Long());
            AddColumn("dbo.LegalEntity", "RoleIsAuthorisedRepresentativeFlag", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Units", "Unit_UnitId");
            CreateIndex("dbo.LegalEntity", "Unit_UnitId");
            AddForeignKey("dbo.Units", "Unit_UnitId", "dbo.Units", "UnitId");
            AddForeignKey("dbo.LegalEntity", "Unit_UnitId", "dbo.Units", "UnitId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LegalEntity", "Unit_UnitId", "dbo.Units");
            DropForeignKey("dbo.Units", "Unit_UnitId", "dbo.Units");
            DropIndex("dbo.LegalEntity", new[] { "Unit_UnitId" });
            DropIndex("dbo.Units", new[] { "Unit_UnitId" });
            DropColumn("dbo.LegalEntity", "RoleIsAuthorisedRepresentativeFlag");
            DropColumn("dbo.LegalEntity", "Unit_UnitId");
            DropColumn("dbo.Units", "Unit_UnitId");
        }
    }
}
