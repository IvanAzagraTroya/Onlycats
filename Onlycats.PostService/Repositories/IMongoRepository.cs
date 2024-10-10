namespace OnlycatsTFG.PostService.Repositories
{
    public interface IMongoRepository<T, Key> where T : class
    {
        Task CreateAsync(T entity);
        Task<T> ReadByIdAsync(Key id);
        Task UpdateAsync(Key id, T entity);
        Task DeleteAsync(Key id);
    }
}
