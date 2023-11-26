namespace project_3_quiz.ViewModels
{
    public class QuizVM
    {
        public Guid Id { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public int TimeLimitMin { get; set; }
        public List<ScoreDto> Scores { get; set; }
    }
}
