using System;
using System.IO;

using Microsoft.EntityFrameworkCore;

using KrakenMPSPBusiness.Models;

namespace KrakenMPSPBusiness.Context
{
    public class SqlLiteContext : DbContext
    {
        public DbSet<LegalPersonModel> LegalPerson { get; set; }
        public DbSet<PhysicalPersonModel> PhysicalPerson { get; set; }
        public DbSet<ResourcesFoundModel> ResourcesFound { get; set; }

        private readonly string _connection = $@"{AppDomain.CurrentDomain.BaseDirectory}/database.db";

        public SqlLiteContext()
        {
            try
            {
                var directoryPath = Path.GetDirectoryName(_connection);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                if (File.Exists(_connection))
                {
                    File.Delete(_connection);
                }

                File.Create(_connection);

                //Database.Migrate();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error creating SqLite database: {0}", e.Message);
            }
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={_connection}");
        }
    }
}
