﻿using System;

using Microsoft.EntityFrameworkCore;

using KrakenMPSPCrawler.Models;
using KrakenMPSPConsole.Models;

namespace KrakenMPSPConsole.Context
{
    public class SqlLiteContext : DbContext
    {
        public DbSet<LegalPersonModel> LegalPerson { get; set; }
        public DbSet<PhysicalPersonModel> PhysicalPerson { get; set; }
        public DbSet<ResourcesFound> ResourcesFound { get; set; }

        private readonly string _connection = $@"{AppDomain.CurrentDomain.BaseDirectory}/database.db";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={_connection}");
        }
    }
}
