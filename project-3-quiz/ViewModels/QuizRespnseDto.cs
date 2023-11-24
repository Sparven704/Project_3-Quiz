namespace project_3_quiz.ViewModels
{
    public class QuizRespnseDto
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Link { get; set; }
        public bool IsMultipleAnswer { get; set; }
        public string[]? Options { get; set; }
    }
}
