namespace project_3_quiz.ViewModels
{
    public class QuestionVM
    {
        public Guid Id { get; set; }
        public Guid QuizId { get; set; }
        public string Question {  get; set; }
        public string Answer { get; set; }
        public bool IsAnswerSubmitted { get; set; }
        public bool IsMultipleAnswer { get; set; }
        public List<OptionVM> Options { get; set; }
        public bool HasMedia { get; set; }
        public FetchMediaDto Media { get; set; }
    }
}
