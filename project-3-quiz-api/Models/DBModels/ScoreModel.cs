using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace project_3_quiz_api.Models.DBModels
{
    public class ScoreModel
    {
        //Primary key
        [Key]
        public Guid Id { get; set; }

        //Properties
        [NotNull]
        public int Score { get; set; }

        //Foreign Keys
        public Guid UserId { get; set; }
        public Guid QuizId { get; set; }

        // Navigation Properties
        public virtual QuizModel Quizzes { get; set; }
        public virtual UserModel Users { get; set; }
    }
}
