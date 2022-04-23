namespace PRC.PacketBatchFiller.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Forty : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransferReasonDocuments",
                c => new
                    {
                        TransferReasonDocumentId = c.Long(nullable: false, identity: true),
                        Number = c.String(),
                        Date = c.DateTime(precision: 7, storeType: "datetime2"),
                        TransferReasonType_TransferReasonTypeId = c.Long(),
                    })
                .PrimaryKey(t => t.TransferReasonDocumentId)
                .ForeignKey("dbo.TransferReasonTypes", t => t.TransferReasonType_TransferReasonTypeId)
                .Index(t => t.TransferReasonType_TransferReasonTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransferReasonDocuments", "TransferReasonType_TransferReasonTypeId", "dbo.TransferReasonTypes");
            DropIndex("dbo.TransferReasonDocuments", new[] { "TransferReasonType_TransferReasonTypeId" });
            DropTable("dbo.TransferReasonDocuments");
        }
    }
}
