using OnlycatsTFG.models;
using MongoDB.Driver;

namespace OnlycatsTFG.repository.mongorepository{
    public class ActivityRepository: IMongoRepository<Activity>{
        private readonly IMongoCollection<Activity> _collection;

        public ActivityRepository(IMongoDatabase database, string collectionName) {
            _collection = database.GetCollection<Activity>(collectionName);
        }

        public async Task<Activity> GetByIdAsync(string id) {
            return await _collection.Find(Builders<Activity>.Filter.Eq("Id", id)).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Activity>> GetAllAsync() {
            return await _collection.Find(Builders<Activity>.Filter.Empty).ToListAsync();
        }
        public async Task CreateAsync(Activity item) {
            await _collection.InsertOneAsync(item);
        }
        public async Task UpdateAsync(string id, Activity item) {
            await _collection.ReplaceOneAsync(Builders<Activity>.Filter.Eq("Id", id), item);
        }
        public async Task DeleteAsync(string id) {
            await _collection.DeleteOneAsync(Builders<Activity>.Filter.Eq("Id", id));
        }

    }
}