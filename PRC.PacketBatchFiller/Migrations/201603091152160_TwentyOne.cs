namespace PRC.PacketBatchFiller.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TwentyOne : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Units", "Unit_UnitId", "dbo.Units");
            DropIndex("dbo.Units", new[] { "Unit_UnitId" });
            AddColumn("dbo.AuthorizesDocuments", "Unit_UnitId", c => c.Long());
            AddColumn("dbo.Units", "RoleIsFirstPersonOfTheCompany", c => c.Boolean(nullable: false));
            CreateIndex("dbo.AuthorizesDocuments", "Unit_UnitId");
            AddForeignKey("dbo.AuthorizesDocuments", "Unit_UnitId", "dbo.Units", "UnitId");
            DropColumn("dbo.Units", "Unit_UnitId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Units", "Unit_UnitId", c => c.Long());
            DropForeignKey("dbo.AuthorizesDocuments", "Unit_UnitId", "dbo.Units");
            DropIndex("dbo.AuthorizesDocuments", new[] { "Unit_UnitId" });
            DropColumn("dbo.Units", "RoleIsFirstPersonOfTheCompany");
            DropColumn("dbo.AuthorizesDocuments", "Unit_UnitId");
            CreateIndex("dbo.Units", "Unit_UnitId");
            AddForeignKey("dbo.Units", "Unit_UnitId", "dbo.Units", "UnitId");
        }
    }
}
