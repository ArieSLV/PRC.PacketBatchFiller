namespace PRC.PacketBatchFiller.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Nine : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegistrationCertificateIssuer", "Value", c => c.String());
            DropColumn("dbo.RegistrationCertificateIssuer", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RegistrationCertificateIssuer", "Name", c => c.String());
            DropColumn("dbo.RegistrationCertificateIssuer", "Value");
        }
    }
}
