using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Driver;

using KrakenMPSPBusiness.Models;
using KrakenMPSPBusiness.Context;

namespace KrakenMPSPBusiness.Repository
{
    public class ResourcesFoundRepository
    {
        //private readonly DatabaseContext _contextSelected = DatabaseContext.MongoDb;
        private readonly MongoDbContext _repository;

        public ResourcesFoundRepository()
        {
            _repository = new MongoDbContext();
        }

        public Task<List<ResourcesFoundModel>> GetAll()
        {
            return _repository.ResourcesFound.Find(new BsonDocument()).ToListAsync();
        }

        public Task<ResourcesFoundModel> FindById(Guid id)
        {
            return _repository.ResourcesFound.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public bool UpdateById(Guid id, ResourcesFoundModel resourcesFound)
        {
            var result = _repository.ResourcesFound.ReplaceOne(x => x.Id == id, resourcesFound);
            return result.MatchedCount != 0;
        }

        public bool Save(ResourcesFoundModel resourcesFound)
        {
            return _repository.ResourcesFound.InsertOneAsync(resourcesFound).IsCompleted;
        }

        public bool Delete(ResourcesFoundModel personModel)
        {
            var result = _repository.ResourcesFound.DeleteOne(x => x.Id == personModel.Id);
            return result.DeletedCount != 0;
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
