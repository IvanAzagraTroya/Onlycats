using Microsoft.EntityFrameworkCore;
using OnlycatsTFG.models;

 namespace OnlycatsTFG.repository{
     public class UserRepository : IRepository<int, User>{
         private readonly DbContext _context;
         private readonly DbSet<User> _dbSet;

         public UserRepository(DbContext context) {
             _context = context;
             _dbSet = _context.Users;
         }

         public async Task Create(User entity) {
             await _dbSet.AddAsync(entity);
             await _context.SaveChangesAsync();
         }
         public async Task<IEnumerable<User>> ReadAll() {
             return await _dbSet.ToListAsync();
         }

         public async Task<User> Read(int id){
             return await _dbSet.FirstOrDefault(u => u.UserId == id);
         }

         public async void Update(int id) {
             var result = _dbSet.FirstOrDefault(u => u.UserId == id);
             if(result != null) {
                 _dbSet.Update(result);
                 await _context.SaveChangesAsync();
             }
         }
         public async Task Delete(int id) {
             var result = _dbSet.FirstOrDefault(u => u.UserId == id);
             if(result != null) {
                 _dbSet.Remove(result);
                 await _context.SaveChangesAsync();
             }
         }
     }
 }