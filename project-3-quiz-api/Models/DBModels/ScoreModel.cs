using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace project_3_quiz_api.Models.DBModels
{
    public class ScoreModel
    {
        //Primary key
        [Key]
        public int Id { get; set; }

        //Properties
        [NotNull]
        public int Score { get; set; }

        //Foreign Keys
        //[NotNull]
        //public Guid UserId { get; set; }
        [NotNull]
        public Guid QuizId { get; set; }

        // Navigation Properties
        public virtual QuizModel Quizzes { get; set; }
        // public virtual UserModel Users { get; set; }
    }
}
