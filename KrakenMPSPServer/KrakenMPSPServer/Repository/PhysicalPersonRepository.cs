using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using MongoDB.Driver;

using KrakenMPSPBusiness.Models;

using KrakenMPSPServer.Enum;
using KrakenMPSPServer.Context;

namespace KrakenMPSPServer.Repository
{
    public class PhysicalPersonRepository
    {
        private readonly DatabaseContext _contextSelected = DatabaseContext.MongoDb;

        public PhysicalPersonRepository()
        {
        }

        public Task<List<PhysicalPersonModel>> GetAll()
        {
            return new MongoDbContext().PhysicalPerson.Find(x => true).ToListAsync();
        }

        public Task<PhysicalPersonModel> FindById(Guid id)
        {
            return new MongoDbContext().PhysicalPerson.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateById(Guid id, PhysicalPersonModel physicalPerson)
        {
            var result = await new MongoDbContext().PhysicalPerson.ReplaceOneAsync(x => x.Id == id, physicalPerson);
            return result != null;
        }

        public async Task<bool> Save(PhysicalPersonModel physicalPerson)
        {
            await new MongoDbContext().PhysicalPerson.InsertOneAsync(physicalPerson);
            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await new MongoDbContext().LegalPerson.DeleteOneAsync(x => x.Id == id);
            return result.DeletedCount != 0;
        }
    }
}
