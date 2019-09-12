using System;

using Microsoft.EntityFrameworkCore;

using KrakenMPSPCrawler.Models;

namespace KrakenMPSPConsole
{
    public class DataBaseContext : DbContext
    {
        private readonly string _pathDataBase = $@"{AppDomain.CurrentDomain.BaseDirectory}/temp/database.db";

        public DbSet<LegalPersonModel> LegalPerson { get; set; }
        public DbSet<PhysicalPersonModel> PhysicalPerson { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={_pathDataBase}");
        }
    }
}
