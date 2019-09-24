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
    public class ResourcesFoundRepository
    {
        private readonly DatabaseContext _contextSelected = DatabaseContext.MongoDb;

        public ResourcesFoundRepository()
        {
            if (_contextSelected == DatabaseContext.SqLite)
            {
                new SqlLiteContext().Database.Migrate();
            }
        }

        public Task<List<ResourcesFoundModel>> GetAll()
        {
            if (_contextSelected == DatabaseContext.SqLite)
            {
                return new SqlLiteContext().ResourcesFound.ToListAsync();
            }
            else
            {
                return new MongoDbContext().ResourcesFound.Find(new BsonDocument()).ToListAsync();
            }
        }

        public Task<ResourcesFoundModel> FindById(Guid id)
        {
            if (_contextSelected == DatabaseContext.SqLite)
            {
                return new SqlLiteContext().ResourcesFound.FindAsync(id);
            }
            else
            {
                return new MongoDbContext().ResourcesFound.Find(x => x.Id == id).FirstOrDefaultAsync();
            }
        }

        public bool UpdateById(Guid id, ResourcesFoundModel resourcesFound)
        {
            if (_contextSelected == DatabaseContext.SqLite)
            {
                var context = new SqlLiteContext();
                context.Entry(resourcesFound).State = EntityState.Modified;

                var result = context.SaveChanges();
                return result != 0;
            }
            else
            {
                var result = new MongoDbContext().ResourcesFound.ReplaceOne(x => x.Id == id, resourcesFound);
                return result != null;
            }
        }

        public bool Save(ResourcesFoundModel resourcesFound)
        {
            if (_contextSelected == DatabaseContext.SqLite)
            {
                var context = new SqlLiteContext();

                context.ResourcesFound.Add(resourcesFound);
                var result = context.SaveChanges();
                return result != null;
            }
            else
            {
                var result = new MongoDbContext().ResourcesFound.InsertOneAsync(resourcesFound);
                return result != null;
            }
        }

        public bool Delete(ResourcesFoundModel personModel)
        {
            if (_contextSelected == DatabaseContext.SqLite)
            {
                var context = new SqlLiteContext();

                context.ResourcesFound.Remove(personModel);
                var result = context.SaveChanges();
                return result != null;
            }
            else
            {
                var result = new MongoDbContext().ResourcesFound.DeleteOne(x => x.Id == personModel.Id);
                return result.DeletedCount != 0;
            }
        }
    }

    /*
        IList<TType> GetAll(int take, int page);

        void Create(TType entity);

        void Save(TType entity);
        void Update(TType entity);
        void SaveOrUpdate(TType entity);

        void Delete(TType entity);

        void Merge(TType entity);

        TType Get(Expression<Func<TType, bool>> whereExpression);

        bool Exists(Expression<Func<TType, bool>> whereExpression);
    */
}
