namespace PRC.PacketBatchFiller.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FortyThree : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ShareholderTransferOrders", "ShareholderAccount_ShareholderAccountId", "dbo.ShareholderAccount");
            DropIndex("dbo.ShareholderTransferOrders", new[] { "ShareholderAccount_ShareholderAccountId" });
            DropColumn("dbo.ShareholderTransferOrders", "ShareholderAccount_ShareholderAccountId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShareholderTransferOrders", "ShareholderAccount_ShareholderAccountId", c => c.Long());
            CreateIndex("dbo.ShareholderTransferOrders", "ShareholderAccount_ShareholderAccountId");
            AddForeignKey("dbo.ShareholderTransferOrders", "ShareholderAccount_ShareholderAccountId", "dbo.ShareholderAccount", "ShareholderAccountId");
        }
    }
}
