namespace PRC.PacketBatchFiller.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThirtyTwo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Units", "AuthorizesDocument_DocumentId", "dbo.AuthorizesDocuments");
            DropForeignKey("dbo.AuthorizesDocuments", "Unit_UnitId", "dbo.Units");
            DropForeignKey("dbo.AuthorizesDocuments", "WhoGivingAuthority_UnitId", "dbo.Units");
            DropIndex("dbo.Units", new[] { "AuthorizesDocument_DocumentId" });
            DropIndex("dbo.AuthorizesDocuments", new[] { "Unit_UnitId" });
            DropIndex("dbo.AuthorizesDocuments", new[] { "WhoGivingAuthority_UnitId" });
            CreateTable(
                "dbo.UnitAuthorizesDocuments",
                c => new
                    {
                        Unit_UnitId = c.Long(nullable: false),
                        AuthorizesDocument_DocumentId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Unit_UnitId, t.AuthorizesDocument_DocumentId })
                .ForeignKey("dbo.Units", t => t.Unit_UnitId, cascadeDelete: true)
                .ForeignKey("dbo.AuthorizesDocuments", t => t.AuthorizesDocument_DocumentId, cascadeDelete: true)
                .Index(t => t.Unit_UnitId)
                .Index(t => t.AuthorizesDocument_DocumentId);
            
            CreateTable(
                "dbo.ShareholderAuthorizesDocuments",
                c => new
                    {
                        DocumentId = c.Long(nullable: false),
                        WhoGivingAuthority_ShareholderAccountId = c.Long(),
                    })
                .PrimaryKey(t => t.DocumentId)
                .ForeignKey("dbo.AuthorizesDocuments", t => t.DocumentId)
                .ForeignKey("dbo.ShareholderAccount", t => t.WhoGivingAuthority_ShareholderAccountId)
                .Index(t => t.DocumentId)
                .Index(t => t.WhoGivingAuthority_ShareholderAccountId);
            
            DropColumn("dbo.Units", "AuthorizesDocument_DocumentId");
            DropColumn("dbo.AuthorizesDocuments", "Unit_UnitId");
            DropColumn("dbo.AuthorizesDocuments", "WhoGivingAuthority_UnitId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AuthorizesDocuments", "WhoGivingAuthority_UnitId", c => c.Long());
            AddColumn("dbo.AuthorizesDocuments", "Unit_UnitId", c => c.Long());
            AddColumn("dbo.Units", "AuthorizesDocument_DocumentId", c => c.Long());
            DropForeignKey("dbo.ShareholderAuthorizesDocuments", "WhoGivingAuthority_ShareholderAccountId", "dbo.ShareholderAccount");
            DropForeignKey("dbo.ShareholderAuthorizesDocuments", "DocumentId", "dbo.AuthorizesDocuments");
            DropForeignKey("dbo.UnitAuthorizesDocuments", "AuthorizesDocument_DocumentId", "dbo.AuthorizesDocuments");
            DropForeignKey("dbo.UnitAuthorizesDocuments", "Unit_UnitId", "dbo.Units");
            DropIndex("dbo.ShareholderAuthorizesDocuments", new[] { "WhoGivingAuthority_ShareholderAccountId" });
            DropIndex("dbo.ShareholderAuthorizesDocuments", new[] { "DocumentId" });
            DropIndex("dbo.UnitAuthorizesDocuments", new[] { "AuthorizesDocument_DocumentId" });
            DropIndex("dbo.UnitAuthorizesDocuments", new[] { "Unit_UnitId" });
            DropTable("dbo.ShareholderAuthorizesDocuments");
            DropTable("dbo.UnitAuthorizesDocuments");
            CreateIndex("dbo.AuthorizesDocuments", "WhoGivingAuthority_UnitId");
            CreateIndex("dbo.AuthorizesDocuments", "Unit_UnitId");
            CreateIndex("dbo.Units", "AuthorizesDocument_DocumentId");
            AddForeignKey("dbo.AuthorizesDocuments", "WhoGivingAuthority_UnitId", "dbo.Units", "UnitId");
            AddForeignKey("dbo.AuthorizesDocuments", "Unit_UnitId", "dbo.Units", "UnitId");
            AddForeignKey("dbo.Units", "AuthorizesDocument_DocumentId", "dbo.AuthorizesDocuments", "DocumentId");
        }
    }
}
