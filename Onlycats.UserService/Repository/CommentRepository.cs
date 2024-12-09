using Microsoft.EntityFrameworkCore;
using OnlycatsTFG.models;

 namespace OnlycatsTFG.repository{
     public class CommentRepository : IRepository<int, Comment> {
         private readonly DbContext _context;
         private readonly DbSet<Comment> _dbSet;

         public CommentRepository(DbContext context) {
             _context = context;
             _dbSet = _context.Comments;
         }

         public async Task Create(Comment entity) {
             await _dbSet.AddAsync(entity);
             await _context.SaveChangesAsync();
         }
         public async Task<IEnumerable<Comment>> ReadAll() {
             return await _dbSet.ToListAsync();
        }

        public Comment Read(int id){
             return _dbSet.FirstOrDefault(u => u.CommentId == id);
        }

         public async void Update(int id) {
             var result = _dbSet.FirstOrDefault(u => u.CommentId == id);
             if(result != null) {
                 _dbSet.Update(result);
                 await _context.SaveChangesAsync();
             }
         }
         public async Task Delete(int id) {
             var result = _dbSet.FirstOrDefault(u => u.CommentId == id);
             if(result != null) {
                 _dbSet.Remove(result);
                 await _context.SaveChangesAsync();
             }
         }
     }
 }