using System.ComponentModel.DataAnnotations;

namespace project_3_quiz_api.Models.DBModels
{
    public class MediaModel
    {
        // Primary Key
        [Key]
        public Guid Id { get; set; }

        // Properties
        public string MediaType { get; set; }
        public string MediaUrl { get; set; }

        // Foreign Key
        public Guid QuestionId { get; set; }

        // Navigation Property
        public virtual QuestionModel Question { get; set; }
    }
}
