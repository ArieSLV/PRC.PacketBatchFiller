namespace PRC.PacketBatchFiller.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FortyTwo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Documents", "Signatory_UnitId", c => c.Long());
            CreateIndex("dbo.Documents", "Signatory_UnitId");
            AddForeignKey("dbo.Documents", "Signatory_UnitId", "dbo.Units", "UnitId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Documents", "Signatory_UnitId", "dbo.Units");
            DropIndex("dbo.Documents", new[] { "Signatory_UnitId" });
            DropColumn("dbo.Documents", "Signatory_UnitId");
        }
    }
}
