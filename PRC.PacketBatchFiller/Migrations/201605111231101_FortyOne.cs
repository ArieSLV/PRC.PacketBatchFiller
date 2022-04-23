namespace PRC.PacketBatchFiller.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FortyOne : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ShareholderQuestionnaires", "Signatory_UnitId", "dbo.Persons");
            DropIndex("dbo.ShareholderQuestionnaires", new[] { "Signatory_UnitId" });
            AddColumn("dbo.Documents", "SignedByAuthorizesDocument_DocumentId", c => c.Long());
            AddColumn("dbo.ShareholderTransferOrders", "CreditingAccount_ShareholderAccountId", c => c.Long());
            AddColumn("dbo.ShareholderTransferOrders", "DebitingAccount_ShareholderAccountId", c => c.Long());
            AddColumn("dbo.ShareholderTransferOrders", "AmountOfTransaction", c => c.String());
            AddColumn("dbo.ShareholderTransferOrders", "CashPaymentFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.TransferReasonDocuments", "ShareholderTransferOrder_DocumentId", c => c.Long());
            CreateIndex("dbo.Documents", "SignedByAuthorizesDocument_DocumentId");
            CreateIndex("dbo.TransferReasonDocuments", "ShareholderTransferOrder_DocumentId");
            CreateIndex("dbo.ShareholderTransferOrders", "CreditingAccount_ShareholderAccountId");
            CreateIndex("dbo.ShareholderTransferOrders", "DebitingAccount_ShareholderAccountId");
            AddForeignKey("dbo.Documents", "SignedByAuthorizesDocument_DocumentId", "dbo.AuthorizesDocuments", "DocumentId");
            AddForeignKey("dbo.TransferReasonDocuments", "ShareholderTransferOrder_DocumentId", "dbo.ShareholderTransferOrders", "DocumentId");
            AddForeignKey("dbo.ShareholderTransferOrders", "CreditingAccount_ShareholderAccountId", "dbo.ShareholderAccount", "ShareholderAccountId");
            AddForeignKey("dbo.ShareholderTransferOrders", "DebitingAccount_ShareholderAccountId", "dbo.ShareholderAccount", "ShareholderAccountId");
            DropColumn("dbo.ShareholderQuestionnaires", "Signatory_UnitId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShareholderQuestionnaires", "Signatory_UnitId", c => c.Long());
            DropForeignKey("dbo.ShareholderTransferOrders", "DebitingAccount_ShareholderAccountId", "dbo.ShareholderAccount");
            DropForeignKey("dbo.ShareholderTransferOrders", "CreditingAccount_ShareholderAccountId", "dbo.ShareholderAccount");
            DropForeignKey("dbo.TransferReasonDocuments", "ShareholderTransferOrder_DocumentId", "dbo.ShareholderTransferOrders");
            DropForeignKey("dbo.Documents", "SignedByAuthorizesDocument_DocumentId", "dbo.AuthorizesDocuments");
            DropIndex("dbo.ShareholderTransferOrders", new[] { "DebitingAccount_ShareholderAccountId" });
            DropIndex("dbo.ShareholderTransferOrders", new[] { "CreditingAccount_ShareholderAccountId" });
            DropIndex("dbo.TransferReasonDocuments", new[] { "ShareholderTransferOrder_DocumentId" });
            DropIndex("dbo.Documents", new[] { "SignedByAuthorizesDocument_DocumentId" });
            DropColumn("dbo.TransferReasonDocuments", "ShareholderTransferOrder_DocumentId");
            DropColumn("dbo.ShareholderTransferOrders", "CashPaymentFlag");
            DropColumn("dbo.ShareholderTransferOrders", "AmountOfTransaction");
            DropColumn("dbo.ShareholderTransferOrders", "DebitingAccount_ShareholderAccountId");
            DropColumn("dbo.ShareholderTransferOrders", "CreditingAccount_ShareholderAccountId");
            DropColumn("dbo.Documents", "SignedByAuthorizesDocument_DocumentId");
            CreateIndex("dbo.ShareholderQuestionnaires", "Signatory_UnitId");
            AddForeignKey("dbo.ShareholderQuestionnaires", "Signatory_UnitId", "dbo.Persons", "UnitId");
        }
    }
}
