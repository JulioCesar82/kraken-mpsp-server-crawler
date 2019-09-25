using System;
using System.Threading.Tasks;
using System.Collections.Generic;

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
        }

        public Task<List<LegalPersonModel>> GetAll()
        {
            return new MongoDbContext().LegalPerson.Find(new BsonDocument()).ToListAsync();
        }

        public Task<LegalPersonModel> FindById(Guid id)
        {
            return new MongoDbContext().LegalPerson.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public bool UpdateById(Guid id, LegalPersonModel legalPerson)
        {
            var result = new MongoDbContext().LegalPerson.ReplaceOne(x => x.Id == id, legalPerson);
            return result != null; 
        }

        public bool Save(LegalPersonModel legalPerson)
        {
            var result = new MongoDbContext().LegalPerson.InsertOneAsync(legalPerson);
            return result != null;
        }

        public bool Delete(LegalPersonModel personModel)
        {
            var result = new MongoDbContext().LegalPerson.DeleteOne(x => x.Id == personModel.Id);
            return result.DeletedCount != 0;
        }
    }
}
