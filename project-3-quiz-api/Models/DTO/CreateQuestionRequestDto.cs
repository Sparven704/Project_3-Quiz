namespace project_3_quiz_api.Models.DTO
{
    public class CreateQuestionRequestDto
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public Guid QuizId { get; set; }
        public bool HasMedia { get; set; }
        public bool IsMultipleAnswer { get; set; }
        public string[]? Options { get; set; }
    }
}
