using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace project_3_quiz_api.Models.DBModels
{
    public class UserModel
    {
        // Primary Key
        [Key]
        public Guid Id { get; set; }

        // Navigation Properties
        public IEnumerable<ScoreModel> Scores { get; set; }
        public IEnumerable<QuizModel> Quizzes { get; set; }
    }
}
