namespace PRC.PacketBatchFiller.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThirtyEight : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShareholderTransferOrders",
                c => new
                    {
                        DocumentId = c.Long(nullable: false),
                        IssueOfSecurities_IssueOfSecuritiesId = c.Long(),
                        ShareholderAccount_ShareholderAccountId = c.Long(),
                        QuantityOfTransferedSecurities = c.String(),
                    })
                .PrimaryKey(t => t.DocumentId)
                .ForeignKey("dbo.Documents", t => t.DocumentId)
                .ForeignKey("dbo.IssuesOfSecurities", t => t.IssueOfSecurities_IssueOfSecuritiesId)
                .ForeignKey("dbo.ShareholderAccount", t => t.ShareholderAccount_ShareholderAccountId)
                .Index(t => t.DocumentId)
                .Index(t => t.IssueOfSecurities_IssueOfSecuritiesId)
                .Index(t => t.ShareholderAccount_ShareholderAccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShareholderTransferOrders", "ShareholderAccount_ShareholderAccountId", "dbo.ShareholderAccount");
            DropForeignKey("dbo.ShareholderTransferOrders", "IssueOfSecurities_IssueOfSecuritiesId", "dbo.IssuesOfSecurities");
            DropForeignKey("dbo.ShareholderTransferOrders", "DocumentId", "dbo.Documents");
            DropIndex("dbo.ShareholderTransferOrders", new[] { "ShareholderAccount_ShareholderAccountId" });
            DropIndex("dbo.ShareholderTransferOrders", new[] { "IssueOfSecurities_IssueOfSecuritiesId" });
            DropIndex("dbo.ShareholderTransferOrders", new[] { "DocumentId" });
            DropTable("dbo.ShareholderTransferOrders");
        }
    }
}
