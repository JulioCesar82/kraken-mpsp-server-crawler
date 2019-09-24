using System;
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

        public Task<LegalPersonModel> FindById(Guid id)
        {
            if (_contextSelected == DatabaseContext.SqLite)
            {
                return new SqlLiteContext().LegalPerson.FindAsync(id);
            }
            else
            {
                return new MongoDbContext().LegalPerson.Find(x => x.Id == id).FirstOrDefaultAsync();
            }
        }

        public bool UpdateById(Guid id, LegalPersonModel legalPerson)
        {
            if (_contextSelected == DatabaseContext.SqLite)
            {
                var context = new SqlLiteContext();
                context.Entry(legalPerson).State = EntityState.Modified;

                var result = context.SaveChanges();
                return result != 0;
            }
            else
            {
                 var result = new MongoDbContext().LegalPerson.ReplaceOne(x => x.Id == id, legalPerson);
                 return result != null;
            }
        }

        public bool Save(LegalPersonModel legalPerson)
        {
            if (_contextSelected == DatabaseContext.SqLite)
            {
                var context = new SqlLiteContext();

                context.LegalPerson.Add(legalPerson);
                var result = context.SaveChanges();
                return result != null;
            }
            else
            {
                var result = new MongoDbContext().LegalPerson.InsertOneAsync(legalPerson);
                return result != null;
            }
        }

        public bool Delete(LegalPersonModel personModel)
        {
            if (_contextSelected == DatabaseContext.SqLite)
            {
                var context = new SqlLiteContext();

                context.LegalPerson.Remove(personModel);
                var result = context.SaveChanges();
                return result != null;
            }
            else
            {
                var result = new MongoDbContext().LegalPerson.DeleteOne(x => x.Id == personModel.Id);
                return result.DeletedCount != 0;
            }
        }
    }
}
