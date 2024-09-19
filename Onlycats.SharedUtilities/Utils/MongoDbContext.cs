using System.Diagnostics;
using MongoDB.Driver;

namespace OnlycatsTFG.utils{
    public class MongoDbContext {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionString, string databaseName) {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Activity> activities => _database.GetCollection<Activity>("activities");
    }
}