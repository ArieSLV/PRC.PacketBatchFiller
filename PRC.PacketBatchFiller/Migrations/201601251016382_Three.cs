namespace PRC.PacketBatchFiller.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Three : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Email", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Email", "Type", c => c.String());
        }
    }
}
