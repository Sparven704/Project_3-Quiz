using System.ComponentModel.DataAnnotations;

namespace project_3_quiz_api.Models.DBModels
{
    public class ContentTypeModel
    {
        // Primary Key
        [Key]
        public Guid Id { get; set; }

        // Properties
        public bool Image { get; set; }
        public bool Video { get; set; }
        public bool IsMultipleAnswers { get; set; }

        // Navigation Properties
        public IEnumerable<QuestionModel> Questions { get; set; }
    }
}
