namespace OnlycatsTFG.PostService.Repositories
{
    public interface IMongoRepository<T, Key> where T : class
    {
        Task<List<T>> ReadAll();
        Task CreateAsync(T entity);
        Task<T> ReadByIdAsync(string id);
        Task UpdateAsync(string id, T entity);
        Task DeleteAsync(string id);
        Task<List<T>> GetByOtherIdAsync(Key id);
        Task<List<T>> GetByOtherIdAsync(int id);
    }
}
