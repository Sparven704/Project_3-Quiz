using System.ComponentModel.DataAnnotations;

namespace project_3_quiz_api.Models.DBModels
{
    public class QuestionModel
    {
        // Primary Key
        [Key]
        public Guid Id { get; set; }
        // Properties
        public string Question { get; set; }
        public string Answer { get; set; }
        public string? link { get; set; }
        public bool IsMultipleAnswer { get; set; }
        // Foreign Keys

        public Guid QuizId { get; set; }

        // Navigation Properties
        public virtual QuizModel Quizzes { get; set; }
        public virtual IEnumerable<OptionModel> Options { get; set; }
    }
}
