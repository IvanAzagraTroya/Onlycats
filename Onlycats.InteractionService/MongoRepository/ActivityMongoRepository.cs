using MongoDB.Driver;
using OnlycatsTFG.PostService.Repositories;
using OnlycatsTFG.models;

namespace OnlycatsTFG.InteractionService.MongoRepository
{
    public class ActivityMongoRepository<T, Key> : IMongoRepository<T, Key> where T : Activity
    {
        private readonly IMongoCollection<T> _collection;

        public ActivityMongoRepository(IMongoCollection<T> collection)
        {
            _collection = collection;
        }

        public async Task<T> ReadByIdAsync(Key id)
        {
            return  await (Task<T>) _collection.Find(Builders<T>.Filter.Eq("_id", id)); //_collection.AsQueryable().Where(x => x.ActivityId == id).FirstOrDefault();
        }

        public async Task CreateAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(Key id, T entity)
        {
            await _collection.ReplaceOneAsync(
                Builders<T>.Filter.Eq("_id", id),
                entity);
        }

        public async Task DeleteAsync(Key id)
        {
            await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", id));
        }
    }
}
