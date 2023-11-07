using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace project_3_quiz_api.Models.DBModels
{
    public class UserModel
    {
        // Primary Key
        [Key]
        public Guid Id { get; set; }

        // Properties
        [NotNull]
        public string Username { get; set; }
        [NotNull]
        public string Password { get; set; }

        // Foreign Key
        [NotNull]
        public Guid RoleId { get; set; }

        // Navigation Properties
        public IEnumerable<ScoreModel> Scores { get; set; }
        public IEnumerable<QuizModel> Quizzes { get; set; }
        public virtual RoleModel Roles { get; set; }
    }
}
