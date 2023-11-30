namespace project_3_quiz.ViewModels
{
    public class FetchQuizQuestionsResponseDto
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Link { get; set; }
        public string[]? Options { get; set; }
        public bool HasMedia { get; set; }
        public Guid? MediaId { get; set; }
    }
}
