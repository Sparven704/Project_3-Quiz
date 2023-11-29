namespace project_3_quiz.ViewModels
{
    public class RequestDTO
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public Guid QuizId { get; set; }
        public string? Link { get; set; }
        public string[]? Options { get; set; }
        public bool IsMultipleAnswer { get; set; }
        public bool HasMedia { get; set; }
    }
}
