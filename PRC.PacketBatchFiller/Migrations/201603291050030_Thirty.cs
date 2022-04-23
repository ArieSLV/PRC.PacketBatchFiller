namespace PRC.PacketBatchFiller.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Thirty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShareholderQuestionnaires", "NotificationReceivingMethod", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShareholderQuestionnaires", "NotificationReceivingMethod");
        }
    }
}
