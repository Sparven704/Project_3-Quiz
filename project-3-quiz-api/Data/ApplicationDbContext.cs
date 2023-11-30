using Microsoft.EntityFrameworkCore;
using project_3_quiz_api.Models.DBModels;

namespace project_3_quiz_api.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Database Tables
        public DbSet<OptionModel> Options { get; set; }
        public DbSet<QuizModel> Quizzes { get; set; }
        public DbSet<QuestionModel> Questions { get; set; }
        public DbSet<ScoreModel> Scores { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<MediaModel> Media { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // OptionModel
            modelBuilder.Entity<OptionModel>()
                .HasOne(c => c.Questions)
                .WithMany(o => o.Options)
                .HasForeignKey(c => c.QuestionId);

            // QuestionModel
            modelBuilder.Entity<QuestionModel>()
                .HasOne(q => q.Quizzes)
                .WithMany(c => c.Questions)
                .HasForeignKey(q => q.QuizId);

            modelBuilder.Entity<QuestionModel>()
                .HasOne(q => q.Media)
                .WithMany(m => m.Questions)
                .HasForeignKey(q => q.MediaId);

            // QuizModel
            modelBuilder.Entity<QuizModel>()
                .HasOne(qz => qz.Users)
                .WithMany(u => u.Quizzes)
                .HasForeignKey(qz => qz.UserId);

            // ScoreModel
            modelBuilder.Entity<ScoreModel>()
                .HasOne(s => s.Users)
                .WithMany(u => u.Scores)
                .HasForeignKey(s => s.UserId);

            modelBuilder.Entity<ScoreModel>()
                .HasOne(s => s.Quizzes)
                .WithMany(qz => qz.Scores)
                .HasForeignKey(s => s.QuizId);


        }
    }
}
