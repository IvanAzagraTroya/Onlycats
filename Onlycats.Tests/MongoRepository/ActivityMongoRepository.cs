﻿using MongoDB.Driver;
using OnlycatsTFG.repository.mongorepository;
using OnlycatsTFG.models;
using MongoDB.Bson;

namespace OnlycatsTFG.InteractionService.MongoRepository
{
    public class ActivityMongoRepository<T, Key> : IMongoRepository<T, Key> where T : Activity
    {
        private readonly IMongoCollection<T> _collection;

        public ActivityMongoRepository(IMongoCollection<T> collection)
        {
            _collection = collection;
        }

        public async Task<List<T>> ReadAll()
        {
            return await (Task<List<T>>)_collection.Find(_ => true).ToListAsync();
        }
        public async Task<T> ReadByIdAsync(Key id)
        {
            return await _collection.Find(Builders<T>.Filter.Eq("_id", id)).FirstOrDefaultAsync();
            //return  await (Task<T>) _collection.Find(Builders<T>.Filter.Eq("_id", id)); //_collection.AsQueryable().Where(x => x.ActivityId == id).FirstOrDefault();
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

        public async Task<List<T>> GetInteractionsByUserId(int userId)
        {
            return await _collection.Find(Builders<T>.Filter.Eq("UserId", userId)).ToListAsync();
        }

        public async Task<List<T>> GetPostInteractionsOrderedByDateAsync(Key postId)
        {
            var sort = Builders<T>.Sort.Descending(i => i.ActivityDate);

            return await _collection.Find(Builders<T>.Filter.Eq("PostId", postId))
            .Sort(sort)
            .ToListAsync();
        }

        public async Task<List<T>> GetByInteractionType(int type)
        {
            var _type = type switch
            {
                1 => ActivityType.Post,
                2 => ActivityType.Like,
                3 => ActivityType.Comment,
                4 => ActivityType.Delete,
                5 => ActivityType.Follow,
                6 => ActivityType.Unfollow,
                _ => ActivityType.NoActivity,
            };
            return await _collection.Find(Builders<T>.Filter.Eq(i => i.ActionType, _type)).ToListAsync();
        }

        public async Task<List<T>> GetByOtherIdAsync(Key id)
        {
            return await _collection.Find(Builders<T>.Filter.Eq("PostId", id)).ToListAsync();
        }
    }
}
