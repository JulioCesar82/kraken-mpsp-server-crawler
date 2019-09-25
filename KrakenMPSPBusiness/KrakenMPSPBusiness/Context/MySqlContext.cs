using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

using KrakenMPSPBusiness.Models;

namespace KrakenMPSPBusiness.Context
{
    public class MySqlContext : DbContext
    {
        public DbSet<LegalPersonModel> LegalPerson { get; set; }
        public DbSet<PhysicalPersonModel> PhysicalPerson { get; set; }
        public DbSet<ResourcesFoundModel> ResourcesFound { get; set; }

        private readonly string _connection = $@"{AppDomain.CurrentDomain.BaseDirectory}/database.db";

        public MySqlContext() : base("MySqlContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
