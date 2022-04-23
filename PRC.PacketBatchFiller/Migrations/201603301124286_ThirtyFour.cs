namespace PRC.PacketBatchFiller.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThirtyFour : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UnitAuthorizesDocuments", "Unit_UnitId", "dbo.Units");
            DropForeignKey("dbo.UnitAuthorizesDocuments", "AuthorizesDocument_DocumentId", "dbo.AuthorizesDocuments");
            DropIndex("dbo.UnitAuthorizesDocuments", new[] { "Unit_UnitId" });
            DropIndex("dbo.UnitAuthorizesDocuments", new[] { "AuthorizesDocument_DocumentId" });
            AddColumn("dbo.Units", "AuthorizesDocument_DocumentId", c => c.Long());
            CreateIndex("dbo.Units", "AuthorizesDocument_DocumentId");
            AddForeignKey("dbo.Units", "AuthorizesDocument_DocumentId", "dbo.AuthorizesDocuments", "DocumentId");
            DropTable("dbo.UnitAuthorizesDocuments");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UnitAuthorizesDocuments",
                c => new
                    {
                        Unit_UnitId = c.Long(nullable: false),
                        AuthorizesDocument_DocumentId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Unit_UnitId, t.AuthorizesDocument_DocumentId });
            
            DropForeignKey("dbo.Units", "AuthorizesDocument_DocumentId", "dbo.AuthorizesDocuments");
            DropIndex("dbo.Units", new[] { "AuthorizesDocument_DocumentId" });
            DropColumn("dbo.Units", "AuthorizesDocument_DocumentId");
            CreateIndex("dbo.UnitAuthorizesDocuments", "AuthorizesDocument_DocumentId");
            CreateIndex("dbo.UnitAuthorizesDocuments", "Unit_UnitId");
            AddForeignKey("dbo.UnitAuthorizesDocuments", "AuthorizesDocument_DocumentId", "dbo.AuthorizesDocuments", "DocumentId", cascadeDelete: true);
            AddForeignKey("dbo.UnitAuthorizesDocuments", "Unit_UnitId", "dbo.Units", "UnitId", cascadeDelete: true);
        }
    }
}
