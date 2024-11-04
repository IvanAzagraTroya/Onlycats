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
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(255);

                entity.Property(e => e.UserName)
                    .HasMaxLength(255);

                entity.Property(e => e.ProfilePicture)
                    .HasMaxLength(255);

                entity.Property(e => e.FollowerNum)
                    .HasDefaultValue(0);

                entity.Property(e => e.FollowingNum)
                    .HasDefaultValue(0);

                entity.Property(e => e.PostNum)
                    .HasDefaultValue(0);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasIndex(e => e.Email)
                    .IsUnique();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.JoinedDate);
            });
        }
    }
}
