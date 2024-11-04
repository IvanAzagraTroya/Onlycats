using MongoDB.Driver;
using Onlycats.PostService.Models;
using OnlycatsTFG.PostService.Repositories;

namespace OnlycatsTFG.PostService.MongoRepository
{
    public class ImageMongoRepository<T, Key> : IMongoRepository<T, Key> where T : Image
    {
        private readonly IMongoCollection<T> _collection;

        public ImageMongoRepository(IMongoCollection<T> collection)
        {
            _collection = collection;
        }

        public async Task<List<T>> ReadAll()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }
        public async Task<T> ReadByIdAsync(Key id)
        {
            return await (Task<T>)_collection.Find(Builders<T>.Filter.Eq("_id", id)); //_collection.AsQueryable().Where(x => x.ImageId == id).FirstOrDefault();
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

        public async Task<List<T>> GetByOtherIdAsync(Key id)
        {
            return await _collection.Find(Builders<T>.Filter.Eq("PostId", id)).ToListAsync();
        }

        public async Task<List<T>> GetByOtherIdAsync(int id)
        {
            return await _collection.Find(Builders<T>.Filter.Eq("UserId", id)).ToListAsync();
        }
    }
}
