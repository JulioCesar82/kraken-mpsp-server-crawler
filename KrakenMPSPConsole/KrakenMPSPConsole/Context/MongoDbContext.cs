using System;
using System.Security.Authentication;

using MongoDB.Driver;

using KrakenMPSPConsole.Models;
using KrakenMPSPCrawler.Models;

namespace KrakenMPSPConsole.Context
{
    public class MongoDbContext : IDisposable
    {
        public IMongoCollection<LegalPersonModel> LegalPerson => Database.GetCollection<LegalPersonModel>("LegalPerson");
        public IMongoCollection<PhysicalPersonModel> PhysicalPerson => Database.GetCollection<PhysicalPersonModel>("PhysicalPerson");
        public IMongoCollection<ResourcesFound> ResourcesFound => Database.GetCollection<ResourcesFound>("ResourcesFound");

        private readonly string _connection = "mongodb://localhost:27017";
        public IMongoDatabase Database { get; }

        public MongoDbContext()
        {
            var DatabaseName = "Kraken";
            bool IsSSL = true;

            try
            {
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(_connection));
                if (IsSSL)
                {
                    settings.SslSettings = new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 };
                }
                var mongoClient = new MongoClient(settings);
                Database = mongoClient.GetDatabase(DatabaseName);
            }
            catch (Exception e)
            {
                throw new Exception("Unable to connect to server.", e);
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
