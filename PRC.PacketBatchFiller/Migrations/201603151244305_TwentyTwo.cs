namespace PRC.PacketBatchFiller.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TwentyTwo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LegalEntities", "FirstPersonOfCompany_UnitId", "dbo.Units");
            DropIndex("dbo.LegalEntities", new[] { "FirstPersonOfCompany_UnitId" });
            DropColumn("dbo.LegalEntities", "FirstPersonOfCompany_UnitId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LegalEntities", "FirstPersonOfCompany_UnitId", c => c.Long());
            CreateIndex("dbo.LegalEntities", "FirstPersonOfCompany_UnitId");
            AddForeignKey("dbo.LegalEntities", "FirstPersonOfCompany_UnitId", "dbo.Units", "UnitId");
        }
    }
}
