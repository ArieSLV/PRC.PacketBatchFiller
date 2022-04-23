namespace PRC.PacketBatchFiller.DataAccess
{
    using System.Data.Entity.Migrations;
    
    public partial class Five : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RegistrationCertificate", "IsDirty");
            DropColumn("dbo.RegistrationCertificate", "IsReadOnly");
            DropColumn("dbo.RegistrationCertificateIssuer", "IsDirty");
            DropColumn("dbo.RegistrationCertificateIssuer", "IsReadOnly");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RegistrationCertificateIssuer", "IsReadOnly", c => c.Boolean(nullable: false));
            AddColumn("dbo.RegistrationCertificateIssuer", "IsDirty", c => c.Boolean(nullable: false));
            AddColumn("dbo.RegistrationCertificate", "IsReadOnly", c => c.Boolean(nullable: false));
            AddColumn("dbo.RegistrationCertificate", "IsDirty", c => c.Boolean(nullable: false));
        }
    }
}
