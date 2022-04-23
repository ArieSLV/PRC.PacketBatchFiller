namespace PRC.PacketBatchFiller.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Seven : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Citizenship", "Value", c => c.String());
            DropColumn("dbo.Citizenship", "CitizenshipValue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Citizenship", "CitizenshipValue", c => c.String());
            DropColumn("dbo.Citizenship", "Value");
        }
    }
}
