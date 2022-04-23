namespace PRC.PacketBatchFiller.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Eleven : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LegalEntity", "RoleIsSecuritiesIssuerFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.LegalEntity", "RoleIsShareholderFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.Person", "RoleIsShareHolderFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.Person", "RoleIsAuthorisedRepresentativeFlag", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Person", "RoleIsAuthorisedRepresentativeFlag");
            DropColumn("dbo.Person", "RoleIsShareHolderFlag");
            DropColumn("dbo.LegalEntity", "RoleIsShareholderFlag");
            DropColumn("dbo.LegalEntity", "RoleIsSecuritiesIssuerFlag");
        }
    }
}
