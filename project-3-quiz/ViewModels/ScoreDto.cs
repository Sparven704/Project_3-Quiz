namespace project_3_quiz.ViewModels
{
    public class ScoreDto
    {
        public Guid QuizId { get; set; }
        public string JwtToken { get; set; }
        public int Points { get; set; }
    }
}
