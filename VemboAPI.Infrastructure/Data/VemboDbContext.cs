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
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseType> ExerciseTypes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<UserPeriodProgress> UserPeriodProgresses { get; set; }
        public DbSet<UserTopicProgress> UserTopicProgresses { get; set; }
        public DbSet<UserUnitProgress> UserUnitProgresses { get; set; }
        public DbSet<UserLevelProgress> UserLevelProgresses { get; set; }
        public DbSet<UserLessonProgress> UserLessonProgresses { get; set; }
        public DbSet<UserExerciseMistake> UserExerciseMistakes { get; set; }
        public DbSet<LevelType> LevelTypes { get; set; }
        public DbSet<GuideBook> GuideBooks { get; set; }
        public DbSet<UserAchievement> UserAchievements { get; set; }
        public DbSet<AchievementLevel> AchievementLevels { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<UserStatistic> UserStatistics { get; set; }
        public DbSet<UserLeaderBoardEntry> UserLeaderBoardEntries { get; set; }

        public VemboDbContext(DbContextOptions<VemboDbContext> options) : base(options)
        {
            // За бажанням: можна видалити EnsureCreated — міграції краще
            // Database.EnsureCreated(); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Налаштування User
            modelBuilder.Entity<User>();
            modelBuilder.Entity<Topic>();
            modelBuilder.Entity<Unit>();
            modelBuilder.Entity<Period>();
            modelBuilder.Entity<Level>();
            modelBuilder.Entity<Lesson>();
            modelBuilder.Entity<Exercise>();
            modelBuilder.Entity<ExerciseType>();
            modelBuilder.Entity<Question>();
            modelBuilder.Entity<Answer>();
            modelBuilder.Entity<UserPeriodProgress>();
            modelBuilder.Entity<UserTopicProgress>();
            modelBuilder.Entity<UserUnitProgress>();
            modelBuilder.Entity<UserLevelProgress>();
            modelBuilder.Entity<UserLessonProgress>();
            modelBuilder.Entity<UserExerciseMistake>();
            modelBuilder.Entity<LevelType>();
            modelBuilder.Entity<GuideBook>();
            modelBuilder.Entity<UserAchievement>();
            modelBuilder.Entity<AchievementLevel>();
            modelBuilder.Entity<Achievement>();
            modelBuilder.Entity<Badge>();
            modelBuilder.Entity<UserStatistic>();
            modelBuilder.Entity<UserLeaderBoardEntry>();
        }
    }
}
