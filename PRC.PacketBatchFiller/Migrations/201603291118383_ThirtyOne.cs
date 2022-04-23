namespace PRC.PacketBatchFiller.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThirtyOne : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ShareholderQuestionnaires", name: "AddressForNotification_AddressId", newName: "MailingAddress_AddressId");
            RenameIndex(table: "dbo.ShareholderQuestionnaires", name: "IX_AddressForNotification_AddressId", newName: "IX_MailingAddress_AddressId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ShareholderQuestionnaires", name: "IX_MailingAddress_AddressId", newName: "IX_AddressForNotification_AddressId");
            RenameColumn(table: "dbo.ShareholderQuestionnaires", name: "MailingAddress_AddressId", newName: "AddressForNotification_AddressId");
        }
    }
}
