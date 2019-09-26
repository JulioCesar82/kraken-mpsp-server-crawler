using System;
using System.Threading.Tasks;
using System.Collections.Generic;

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
            return new MongoDbContext().LegalPerson.Find(x => true).ToListAsync();
        }

        public Task<LegalPersonModel> FindById(Guid id)
        {
            return new MongoDbContext().LegalPerson.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateById(Guid id, LegalPersonModel legalPerson)
        {
            var result = await new MongoDbContext().LegalPerson.ReplaceOneAsync(x => x.Id == id, legalPerson);
            return result != null; 
        }

        public async Task<bool> Save(LegalPersonModel legalPerson)
        {
            await new MongoDbContext().LegalPerson.InsertOneAsync(legalPerson);
            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await new MongoDbContext().LegalPerson.DeleteOneAsync(x => x.Id == id);
            return result.DeletedCount != 0;
        }
    }
}
