﻿using System;
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
    public class PhysicalPersonRepository
    {
        private readonly DatabaseContext _contextSelected = DatabaseContext.MongoDb;

        public PhysicalPersonRepository()
        {
            if (_contextSelected == DatabaseContext.SqLite)
            {
                new SqlLiteContext().Database.Migrate();
            }
        }

        public Task<List<PhysicalPersonModel>> GetAll()
        {
            if (_contextSelected == DatabaseContext.SqLite)
            {
                return new SqlLiteContext().PhysicalPerson.ToListAsync();
            }
            else
            {
                return new MongoDbContext().PhysicalPerson.Find(new BsonDocument()).ToListAsync();
            }
        }

        public Task<PhysicalPersonModel> FindById(Guid id)
        {
            if (_contextSelected == DatabaseContext.SqLite)
            {
                return new SqlLiteContext().PhysicalPerson.FindAsync(id);
            }
            else
            {
                return new MongoDbContext().PhysicalPerson.Find(x => x.Id == id).FirstOrDefaultAsync();
            }
        }

        public bool UpdateById(Guid id, PhysicalPersonModel physicalPerson)
        {
            if (_contextSelected == DatabaseContext.SqLite)
            {
                var context = new SqlLiteContext();
                context.Entry(physicalPerson).State = EntityState.Modified;

                var result = context.SaveChanges();
                return result != 0;
            }
            else
            {
                var result = new MongoDbContext().PhysicalPerson.ReplaceOne(x => x.Id == id, physicalPerson);
                return result != null;
            }
        }

        public bool Save(PhysicalPersonModel physicalPerson)
        {
            if (_contextSelected == DatabaseContext.SqLite)
            {
                var context = new SqlLiteContext();

                context.PhysicalPerson.Add(physicalPerson);
                var result = context.SaveChanges();
                return result != null;
            }
            else
            {
                var result = new MongoDbContext().PhysicalPerson.InsertOneAsync(physicalPerson);
                return result != null;
            }
        }

        public bool Delete(PhysicalPersonModel personModel)
        {
            if (_contextSelected == DatabaseContext.SqLite)
            {
                var context = new SqlLiteContext();

                context.PhysicalPerson.Remove(personModel);
                var result = context.SaveChanges();
                return result != null;
            }
            else
            {
                var result = new MongoDbContext().PhysicalPerson.DeleteOne(x => x.Id == personModel.Id);
                return result.DeletedCount != 0;
            }
        }
    }
}
