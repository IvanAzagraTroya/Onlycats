namespace OnlycatsTFG.repository{
    public interface IRepository<Key, T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(Key id);
        Task<T> GetByEmailAsync(string email);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Key id);
    }

}