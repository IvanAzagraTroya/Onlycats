namespace OnlycatsTFG.repository{
    public interface IRepository<ID, T>{
        public Task Create(T entity);
        public Task<T> Read(ID id);
        public Task Update(ID id);
        public Task Delete(ID id);
    }
}