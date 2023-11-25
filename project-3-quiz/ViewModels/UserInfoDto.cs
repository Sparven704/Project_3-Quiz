namespace project_3_quiz.ViewModels
{
    public class UserInfoDto
    {
        public string UserName { get; set; }
        public string Email {  get; set; }
        public List<ScoreDto> Scores { get; set; }
        public List<QuizVM> CreatedQuizzes { get; set; }
    }
}
