using Microsoft.EntityFrameworkCore;
using Onlycats.UserService.Utils;
using OnlycatsTFG.models;

namespace OnlycatsTFG.repository{
    public class UserRepository<Int, T> : IRepository<int, T> where T : User
    {
         private readonly ApplicationDbContext _context;
         private readonly DbSet<T> _dbSet;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null) { 
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByEmailAsync(string email)
        {
            return await _dbSet.Where(i => i.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
 }