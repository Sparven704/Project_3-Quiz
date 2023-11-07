using Microsoft.EntityFrameworkCore;
using project_3_quiz_api.Models.DBModels;

namespace project_3_quiz_api.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Database Tables
        public DbSet<ContentModel> Contents { get; set; }
        public DbSet<ContentTypeModel> ContentTypes { get; set; }
        public DbSet<QuizModel> Quizzes { get; set; }
        public DbSet<QuestionModel> Questions { get; set; }
        //public DbSet<ScoreModel> Scores { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // QuestionModel
            modelBuilder.Entity<QuestionModel>()
                .HasOne(q => q.ContentTypes)
                .WithMany(ct => ct.Questions)
                .HasForeignKey(q => q.ContentTypeId);

            modelBuilder.Entity<QuestionModel>()
                .HasOne(q => q.Quizzes)
                .WithMany(qz => qz.Questions)
                .HasForeignKey(q => q.QuizId);

            modelBuilder.Entity<QuestionModel>()
                .HasOne(q => q.Contents)
                .WithMany(c => c.Questions)
                .HasForeignKey(q => q.ContentId);

            // QuizModel
            //modelBuilder.Entity<QuizModel>()
            //    .HasOne(qz => qz.Users)
            //    .WithMany(u => u.Quizzes)
            //    .HasForeignKey(qz => qz.UserId);

            // ScoreModel
            //modelBuilder.Entity<ScoreModel>()
            //    .HasOne(s => s.Users)
            //    .WithMany(u => u.Scores)
            //    .HasForeignKey(s => s.UserId);

            //modelBuilder.Entity<ScoreModel>()
            //    .HasOne(s => s.Quizzes)
            //    .WithMany(qz => qz.Scores)
            //    .HasForeignKey(s => s.QuizId);

        }
    }
}
