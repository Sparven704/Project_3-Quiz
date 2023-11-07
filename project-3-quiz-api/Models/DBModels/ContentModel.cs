using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace project_3_quiz_api.Models.DBModels
{
    public class ContentModel
    {
        // Primary Key
        [Key]
        public Guid Id { get; set; }

        // Properties
        public string? Img { get; set; }
        public string? Video { get; set; }
        [NotNull]
        public string Question { get; set; }
        [NotNull]
        public string Answer { get; set; }


        // Navigation Properties
        public IEnumerable<QuestionModel> Questions { get; set; }
    }
}
