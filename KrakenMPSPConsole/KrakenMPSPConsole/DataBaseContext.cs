using System;
using System.IO;

using Microsoft.EntityFrameworkCore;

using KrakenMPSPCrawler.Models;

namespace KrakenMPSPConsole
{
    public class DataBaseContext : DbContext
    {
        private readonly string _pathDataBase = $@"{AppDomain.CurrentDomain.BaseDirectory}/temp/database.db";

        public DbSet<LegalPersonModel> LegalPerson { get; set; }
        public DbSet<PhysicalPersonModel> PhysicalPerson { get; set; }

        public DataBaseContext(bool isDatabase)
        {
            if (isDatabase)
            {
                BuildDataBase();
            }
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={_pathDataBase}");
        }

        private void BuildDataBase()
        {
            try
            {
                var directoryPath = Path.GetDirectoryName(_pathDataBase);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                if (File.Exists(_pathDataBase))
                {
                    File.Delete(_pathDataBase);
                }

                File.Create(_pathDataBase);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error creating SqLite database: {0}", e.Message);
            }
        }

    }
}
