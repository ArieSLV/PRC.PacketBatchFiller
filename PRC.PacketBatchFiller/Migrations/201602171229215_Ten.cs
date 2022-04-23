namespace PRC.PacketBatchFiller.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ten : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LegalEntity", "OKPO", c => c.String());
            AddColumn("dbo.LegalEntity", "OKVED", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LegalEntity", "OKVED");
            DropColumn("dbo.LegalEntity", "OKPO");
        }
    }
}
