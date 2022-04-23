namespace PRC.PacketBatchFiller.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fiveteen : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Units", "RoleIsShareHolderFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.Units", "RoleIsAuthorisedRepresentativeFlag", c => c.Boolean(nullable: false));
            DropColumn("dbo.LegalEntity", "RoleIsShareholderFlag");
            DropColumn("dbo.LegalEntity", "RoleIsAuthorisedRepresentativeFlag");
            DropColumn("dbo.Person", "RoleIsShareHolderFlag");
            DropColumn("dbo.Person", "RoleIsAuthorisedRepresentativeFlag");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Person", "RoleIsAuthorisedRepresentativeFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.Person", "RoleIsShareHolderFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.LegalEntity", "RoleIsAuthorisedRepresentativeFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.LegalEntity", "RoleIsShareholderFlag", c => c.Boolean(nullable: false));
            DropColumn("dbo.Units", "RoleIsAuthorisedRepresentativeFlag");
            DropColumn("dbo.Units", "RoleIsShareHolderFlag");
        }
    }
}
