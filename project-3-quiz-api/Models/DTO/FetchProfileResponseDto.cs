using project_3_quiz_api.Models.DBModels;

namespace project_3_quiz_api.Models.DTO
{
    public class FetchProfileResponseDto
    {
        public string Email { get; set; }
        public string[]? QuizTitle { get; set; }
        public string[]? QuizLink { get; set; }
    }
}
