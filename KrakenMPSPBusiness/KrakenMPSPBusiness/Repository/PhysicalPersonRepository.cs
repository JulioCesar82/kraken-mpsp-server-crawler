using System;
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

        public List<PhysicalPersonModel> GetAll()
        {
            return new MongoDbContext().PhysicalPerson.Find(new BsonDocument()).ToList();
        }

        public PhysicalPersonModel FindById(Guid id)
        {
            return new MongoDbContext().PhysicalPerson.Find(x => x.Id == id).FirstOrDefault();
        }

        public bool UpdateById(Guid id, PhysicalPersonModel physicalPerson)
        {
            var result = new MongoDbContext().PhysicalPerson.ReplaceOne(x => x.Id == id, physicalPerson);
            return result != null;
        }

        public bool Save(PhysicalPersonModel physicalPerson)
        {
            new MongoDbContext().PhysicalPerson.InsertOne(physicalPerson);
            return true;
        }

        public bool Delete(PhysicalPersonModel personModel)
        {
            var result = new MongoDbContext().PhysicalPerson.DeleteOne(x => x.Id == personModel.Id);
            return result.DeletedCount != 0;
        }
    }
}
