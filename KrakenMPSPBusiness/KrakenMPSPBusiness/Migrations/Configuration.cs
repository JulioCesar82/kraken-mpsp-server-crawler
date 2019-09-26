using System.Data.Entity.Migrations;

using KrakenMPSPBusiness.Context;

namespace KrakenMPSPBusiness.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<MySqlContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MySqlContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
