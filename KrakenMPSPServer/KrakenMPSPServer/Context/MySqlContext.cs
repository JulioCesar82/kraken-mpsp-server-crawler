//using System.Data.Entity;
//using System.Data.Entity.ModelConfiguration.Conventions;

using KrakenMPSPBusiness.Models;
using Microsoft.EntityFrameworkCore;

namespace KrakenMPSPServer.Context
{
    public class MySqlContext : DbContext
    {
        public DbSet<LegalPersonModel> LegalPerson { get; set; }
        public DbSet<PhysicalPersonModel> PhysicalPerson { get; set; }
        public DbSet<ResourcesFoundModel> ResourcesFound { get; set; }

        //public MySqlContext() : base("MySqlContext") {}

        /*
         protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
        */
    }
}
