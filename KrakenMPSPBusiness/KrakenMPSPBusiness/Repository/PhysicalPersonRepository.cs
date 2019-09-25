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
    public class PhysicalPersonRepository
    {
        private readonly DatabaseContext _contextSelected = DatabaseContext.MongoDb;

        public PhysicalPersonRepository()
        {
        }

        public Task<List<PhysicalPersonModel>> GetAll()
        {
            return new MongoDbContext().PhysicalPerson.Find(new BsonDocument()).ToListAsync();
        }

        public Task<PhysicalPersonModel> FindById(Guid id)
        {
            return new MongoDbContext().PhysicalPerson.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public bool UpdateById(Guid id, PhysicalPersonModel physicalPerson)
        {
            var result = new MongoDbContext().PhysicalPerson.ReplaceOne(x => x.Id == id, physicalPerson);
            return result != null;
        }

        public bool Save(PhysicalPersonModel physicalPerson)
        {
            var result = new MongoDbContext().PhysicalPerson.InsertOneAsync(physicalPerson);
            return result != null;
        }

        public bool Delete(PhysicalPersonModel personModel)
        {
            var result = new MongoDbContext().PhysicalPerson.DeleteOne(x => x.Id == personModel.Id);
            return result.DeletedCount != 0;
        }
    }
}
