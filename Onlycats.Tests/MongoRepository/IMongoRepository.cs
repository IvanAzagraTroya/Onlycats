namespace OnlycatsTFG.repository.mongorepository{
    public interface IMongoRepository<T, Key> where T : class
    {
        Task<List<T>> ReadAll();
        Task CreateAsync(T entity);
        Task<T> ReadByIdAsync(Key id);
        Task UpdateAsync(Key id, T entity);
        Task DeleteAsync(Key id);
        Task<List<T>> GetInteractionsByUserId(int userId);
        Task<List<T>> GetPostInteractionsOrderedByDateAsync(Key postId);
        Task<List<T>> GetByInteractionType(int type);
        Task<List<T>> GetByOtherIdAsync(Key id);
    }
}