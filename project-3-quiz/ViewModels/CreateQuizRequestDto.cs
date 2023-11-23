namespace project_3_quiz.ViewModels
{
    public class CreateQuizRequestDto
    {
        public string Title { get; set; }
        public int TimeLimitMin { get; set; }
        public string JwtToken { get; set; }
    }
}
