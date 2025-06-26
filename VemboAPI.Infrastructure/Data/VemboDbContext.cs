using Microsoft.EntityFrameworkCore;
using VemboAPI.Domain.Entities;

namespace VemboAPI.Infrastructure.Data 
{
    public class VemboDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<Level> Levels { get; set; }
        public VemboDbContext(DbContextOptions<VemboDbContext> options) : base(options)
        {
            // За бажанням: можна видалити EnsureCreated — міграції краще
            // Database.EnsureCreated(); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Налаштування User
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

            // Автоматично EF Core побудує зв’язок Topic → Parts
            modelBuilder.Entity<Topic>();
            modelBuilder.Entity<Unit>();
            modelBuilder.Entity<Period>();
            modelBuilder.Entity<Level>();
        }
    }
}
