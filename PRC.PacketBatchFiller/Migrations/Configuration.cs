using System.Data.Entity.Migrations;

namespace PRC.PacketBatchFiller.DataAccess
{
    public class Configuration : DbMigrationsConfiguration<PBFContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
        }
    }
}