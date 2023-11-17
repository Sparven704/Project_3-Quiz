namespace project_3_quiz_api.Models.DBModels
{
    public class OptionModel
    {
        //Primary key
        public Guid Id { get; set; }

        //Property
        public string Text { get; set; }

        //Foreign key
        public Guid QuestionId { get; set; }

        //Navigation properties
        public virtual QuestionModel Questions { get; set; }
        public IEnumerable<QuizModel> Quizzes { get; set; }

    }
}
