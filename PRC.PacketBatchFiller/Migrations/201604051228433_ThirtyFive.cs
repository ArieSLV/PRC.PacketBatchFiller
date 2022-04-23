namespace PRC.PacketBatchFiller.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThirtyFive : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Units", "AuthorizesDocument_DocumentId", "dbo.AuthorizesDocuments");
            DropIndex("dbo.Units", new[] { "AuthorizesDocument_DocumentId" });
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
            
            DropColumn("dbo.Units", "AuthorizesDocument_DocumentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Units", "AuthorizesDocument_DocumentId", c => c.Long());
            DropForeignKey("dbo.UnitAuthorizesDocuments", "AuthorizesDocument_DocumentId", "dbo.AuthorizesDocuments");
            DropForeignKey("dbo.UnitAuthorizesDocuments", "Unit_UnitId", "dbo.Units");
            DropIndex("dbo.UnitAuthorizesDocuments", new[] { "AuthorizesDocument_DocumentId" });
            DropIndex("dbo.UnitAuthorizesDocuments", new[] { "Unit_UnitId" });
            DropTable("dbo.UnitAuthorizesDocuments");
            CreateIndex("dbo.Units", "AuthorizesDocument_DocumentId");
            AddForeignKey("dbo.Units", "AuthorizesDocument_DocumentId", "dbo.AuthorizesDocuments", "DocumentId");
        }
    }
}
