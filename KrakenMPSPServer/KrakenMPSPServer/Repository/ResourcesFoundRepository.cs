using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using MongoDB.Driver;

using KrakenMPSPBusiness.Models;

using KrakenMPSPServer.Context;

namespace KrakenMPSPServer.Repository
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
            return _repository.ResourcesFound.Find(x => true).ToListAsync();
        }

        public Task<ResourcesFoundModel> FindById(Guid id)
        {
            return _repository.ResourcesFound.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateById(Guid id, ResourcesFoundModel resourcesFound)
        {
            var result = await _repository.ResourcesFound.ReplaceOneAsync(x => x.Id == id, resourcesFound);
            return result.MatchedCount != 0;
        }

        public async Task<bool> Save(ResourcesFoundModel resourcesFound)
        {
            await _repository.ResourcesFound.InsertOneAsync(resourcesFound);
            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await new MongoDbContext().LegalPerson.DeleteOneAsync(x => x.Id == id);
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
