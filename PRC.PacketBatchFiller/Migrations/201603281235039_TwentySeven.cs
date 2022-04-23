namespace PRC.PacketBatchFiller.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TwentySeven : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShareholderQuestionnaires",
                c => new
                    {
                        DocumentId = c.Long(nullable: false),
                        ShareholderAccount_ShareholderAccountId = c.Long(),
                    })
                .PrimaryKey(t => t.DocumentId)
                .ForeignKey("dbo.Documents", t => t.DocumentId)
                .ForeignKey("dbo.ShareholderAccount", t => t.ShareholderAccount_ShareholderAccountId)
                .Index(t => t.DocumentId)
                .Index(t => t.ShareholderAccount_ShareholderAccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShareholderQuestionnaires", "ShareholderAccount_ShareholderAccountId", "dbo.ShareholderAccount");
            DropForeignKey("dbo.ShareholderQuestionnaires", "DocumentId", "dbo.Documents");
            DropIndex("dbo.ShareholderQuestionnaires", new[] { "ShareholderAccount_ShareholderAccountId" });
            DropIndex("dbo.ShareholderQuestionnaires", new[] { "DocumentId" });
            DropTable("dbo.ShareholderQuestionnaires");
        }
    }
}
