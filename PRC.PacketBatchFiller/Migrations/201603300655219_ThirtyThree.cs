namespace PRC.PacketBatchFiller.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThirtyThree : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DocumentPackages",
                c => new
                    {
                        DocumentPackageId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        TimeStamp = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.DocumentPackageId);
            
            CreateTable(
                "dbo.ShareholderDocumentPackages",
                c => new
                    {
                        DocumentPackageId = c.Long(nullable: false),
                        MainAccount_ShareholderAccountId = c.Long(),
                    })
                .PrimaryKey(t => t.DocumentPackageId)
                .ForeignKey("dbo.DocumentPackages", t => t.DocumentPackageId)
                .ForeignKey("dbo.ShareholderAccount", t => t.MainAccount_ShareholderAccountId)
                .Index(t => t.DocumentPackageId)
                .Index(t => t.MainAccount_ShareholderAccountId);
            
            AddColumn("dbo.Documents", "DocumentPackage_DocumentPackageId", c => c.Long());
            CreateIndex("dbo.Documents", "DocumentPackage_DocumentPackageId");
            AddForeignKey("dbo.Documents", "DocumentPackage_DocumentPackageId", "dbo.DocumentPackages", "DocumentPackageId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShareholderDocumentPackages", "MainAccount_ShareholderAccountId", "dbo.ShareholderAccount");
            DropForeignKey("dbo.ShareholderDocumentPackages", "DocumentPackageId", "dbo.DocumentPackages");
            DropForeignKey("dbo.Documents", "DocumentPackage_DocumentPackageId", "dbo.DocumentPackages");
            DropIndex("dbo.ShareholderDocumentPackages", new[] { "MainAccount_ShareholderAccountId" });
            DropIndex("dbo.ShareholderDocumentPackages", new[] { "DocumentPackageId" });
            DropIndex("dbo.Documents", new[] { "DocumentPackage_DocumentPackageId" });
            DropColumn("dbo.Documents", "DocumentPackage_DocumentPackageId");
            DropTable("dbo.ShareholderDocumentPackages");
            DropTable("dbo.DocumentPackages");
        }
    }
}
