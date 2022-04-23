namespace PRC.PacketBatchFiller.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Two : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Email", "Value", c => c.String());
            AddColumn("dbo.Email", "Type", c => c.String());
            AddColumn("dbo.Email", "Comment", c => c.String());
            AddColumn("dbo.PhoneNumber", "Value", c => c.String());
            AddColumn("dbo.PhoneNumber", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.PhoneNumber", "Comment", c => c.String());
            DropColumn("dbo.Email", "EmailValue");
            DropColumn("dbo.Email", "EmailComment");
            DropColumn("dbo.PhoneNumber", "ContactType");
            DropColumn("dbo.PhoneNumber", "PhoneNumberValue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PhoneNumber", "PhoneNumberValue", c => c.String());
            AddColumn("dbo.PhoneNumber", "ContactType", c => c.Int(nullable: false));
            AddColumn("dbo.Email", "EmailComment", c => c.String());
            AddColumn("dbo.Email", "EmailValue", c => c.String());
            DropColumn("dbo.PhoneNumber", "Comment");
            DropColumn("dbo.PhoneNumber", "Type");
            DropColumn("dbo.PhoneNumber", "Value");
            DropColumn("dbo.Email", "Comment");
            DropColumn("dbo.Email", "Type");
            DropColumn("dbo.Email", "Value");
        }
    }
}
