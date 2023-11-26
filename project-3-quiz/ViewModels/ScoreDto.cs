namespace project_3_quiz.ViewModels
{
    public class ScoreDto
    {
        public string JwtToken { get; set; }
        public Guid QuizId { get; set; }
        public int Score { get; set; }
    }
}
