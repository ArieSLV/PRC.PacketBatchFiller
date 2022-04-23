namespace PRC.PacketBatchFiller.DataAccess
{
    using System.Data.Entity.Migrations;
    
    public partial class TwentyFive : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Units", "RoleIsAuthorisedRepresentativeFlag");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Units", "RoleIsAuthorisedRepresentativeFlag", c => c.Boolean(nullable: false));
        }
    }
}
