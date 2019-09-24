using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using MongoDB.Bson;
using MongoDB.Driver;

using KrakenMPSPBusiness.Enum;
using KrakenMPSPBusiness.Models;
using KrakenMPSPBusiness.Context;

namespace KrakenMPSPBusiness.Repository
{
    public class LegalPersonRepository
    {
        private readonly DatabaseContext _contextSelected = DatabaseContext.MongoDb;

        public LegalPersonRepository()
        {
            if (_contextSelected == DatabaseContext.SqLite)
            {
                new SqlLiteContext().Database.Migrate();
            }
        }

        public Task<List<LegalPersonModel>> GetAll()
        {
            if (_contextSelected == DatabaseContext.SqLite)
            {
                return new SqlLiteContext().LegalPerson.ToListAsync();
            }
            else
            {
                return new MongoDbContext().LegalPerson.Find(new BsonDocument()).ToListAsync();
            }
        }
    }
}
