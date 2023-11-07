using System.ComponentModel.DataAnnotations;

namespace project_3_quiz_api.Models.DBModels
{
    public class QuestionModel
    {
        // Primary Key
        [Key]
        public Guid Id { get; set; }
        // Properties
        public bool IsCorrect { get; set; }

        // Foreign Keys
        public Guid QuizId { get; set; }
        public Guid ContentTypeId { get; set; }
        public Guid ContentId { get; set; }

        // Navigation Properties
        public virtual ContentTypeModel ContentTypes { get; set; }
        public virtual QuizModel Quizzes { get; set; }
        public virtual ContentModel Contents { get; set; }
    }
}
