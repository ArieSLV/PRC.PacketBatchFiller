namespace PRC.PacketBatchFiller.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Eight : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LegalEntity", "FirstPersonOfCompany_UnitId", c => c.Long());
            CreateIndex("dbo.LegalEntity", "FirstPersonOfCompany_UnitId");
            AddForeignKey("dbo.LegalEntity", "FirstPersonOfCompany_UnitId", "dbo.Person", "UnitId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LegalEntity", "FirstPersonOfCompany_UnitId", "dbo.Person");
            DropIndex("dbo.LegalEntity", new[] { "FirstPersonOfCompany_UnitId" });
            DropColumn("dbo.LegalEntity", "FirstPersonOfCompany_UnitId");
        }
    }
}
