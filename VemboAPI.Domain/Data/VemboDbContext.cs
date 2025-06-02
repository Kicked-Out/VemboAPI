using Microsoft.EntityFrameworkCore;
using VemboAPI.Domain.Entities;

namespace VemboAPI.Domain.Data
{
    public class VemboDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.NickName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Password).IsRequired();
                entity.Property(e => e.XP).IsRequired();
                entity.Property(e => e.IsPremium).IsRequired();
                entity.Property(e => e.Rating).IsRequired();
                entity.Property(e => e.Level).IsRequired();
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.UpdatedAt).IsRequired();
            });
        }

        public VemboDbContext(DbContextOptions<VemboDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
