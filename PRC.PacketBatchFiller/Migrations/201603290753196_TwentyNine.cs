namespace PRC.PacketBatchFiller.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TwentyNine : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Documents", "TimeStamp", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Documents", "TimeStamp");
        }
    }
}
