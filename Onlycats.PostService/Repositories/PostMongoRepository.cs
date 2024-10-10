using MongoDB.Driver;
using OnlycatsTFG.PostService.Repositories;

namespace OnlycatsTFG.PostService.MongoRepository
{
    public class PostMongoRepository<T, Key> : IMongoRepository<T, Key> where T : Post
    {
        private readonly IMongoCollection<T> _collection;

        public PostMongoRepository(IMongoCollection<T> collection)
        {
            _collection = collection;
        }

        public async Task<T> ReadByIdAsync(Key id)
        {
            return await (Task<T>)_collection.Find(Builders<T>.Filter.Eq("_id", id)); //_collection.AsQueryable().Where(x => x.PostId == id).FirstOrDefault();
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
