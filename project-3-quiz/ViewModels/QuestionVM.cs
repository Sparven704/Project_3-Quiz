namespace project_3_quiz.ViewModels
{
    public class QuestionVM
    {
        public Guid Id { get; set; }
        public Guid QuizId { get; set; }
        public string Question {  get; set; }
        public string Answer { get; set; }
    }
}
