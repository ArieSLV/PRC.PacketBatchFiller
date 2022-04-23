namespace PRC.PacketBatchFiller.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sixteen : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LegalEntityName", "FullSubname", c => c.String());
            AddColumn("dbo.LegalEntityName", "ShortSubname", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LegalEntityName", "ShortSubname");
            DropColumn("dbo.LegalEntityName", "FullSubname");
        }
    }
}
