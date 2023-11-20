namespace project_3_quiz_api.Models.DTO
{
    public class CreateQuestionRequestDto
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public Guid QuizId { get; set; }
        public string Link { get; set; }
        public string[] Options { get; set; }

    }
}
