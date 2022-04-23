namespace PRC.PacketBatchFiller.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThirtyNine : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransferReasonTypes",
                c => new
                    {
                        TransferReasonTypeId = c.Long(nullable: false, identity: true),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.TransferReasonTypeId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TransferReasonTypes");
        }
    }
}
