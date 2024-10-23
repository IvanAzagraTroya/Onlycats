using Microsoft.EntityFrameworkCore;
using OnlycatsTFG.models;

namespace Onlycats.UserService.Utils
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //faltan cosas
        }
    }
}
