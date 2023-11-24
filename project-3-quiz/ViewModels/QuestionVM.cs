namespace project_3_quiz.ViewModels
{
    public class QuestionVM
    {
        
        public string Question {  get; set; }
        public string Answer { get; set; }
        public bool IsAnswerSubmitted { get; set; }
        public bool IsMultipleAnswer { get; set; }
        public string[]? Options { get; set; }
    }
}
