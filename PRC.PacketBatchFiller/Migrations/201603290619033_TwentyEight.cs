namespace PRC.PacketBatchFiller.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TwentyEight : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShareholderQuestionnaires", "AddressForNotification_AddressId", c => c.Long());
            AddColumn("dbo.ShareholderQuestionnaires", "Signatory_UnitId", c => c.Long());
            AddColumn("dbo.ShareholderQuestionnaires", "SubmittingReason", c => c.Int(nullable: false));
            AddColumn("dbo.ShareholderQuestionnaires", "NotificationRequiredFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.ShareholderQuestionnaires", "AddressForNotificationAsMailingAddressFlag", c => c.Boolean(nullable: false));
            CreateIndex("dbo.ShareholderQuestionnaires", "AddressForNotification_AddressId");
            CreateIndex("dbo.ShareholderQuestionnaires", "Signatory_UnitId");
            AddForeignKey("dbo.ShareholderQuestionnaires", "AddressForNotification_AddressId", "dbo.Addresses", "AddressId");
            AddForeignKey("dbo.ShareholderQuestionnaires", "Signatory_UnitId", "dbo.Persons", "UnitId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShareholderQuestionnaires", "Signatory_UnitId", "dbo.Persons");
            DropForeignKey("dbo.ShareholderQuestionnaires", "AddressForNotification_AddressId", "dbo.Addresses");
            DropIndex("dbo.ShareholderQuestionnaires", new[] { "Signatory_UnitId" });
            DropIndex("dbo.ShareholderQuestionnaires", new[] { "AddressForNotification_AddressId" });
            DropColumn("dbo.ShareholderQuestionnaires", "AddressForNotificationAsMailingAddressFlag");
            DropColumn("dbo.ShareholderQuestionnaires", "NotificationRequiredFlag");
            DropColumn("dbo.ShareholderQuestionnaires", "SubmittingReason");
            DropColumn("dbo.ShareholderQuestionnaires", "Signatory_UnitId");
            DropColumn("dbo.ShareholderQuestionnaires", "AddressForNotification_AddressId");
        }
    }
}
