namespace project_3_quiz_api.Models.DTO
{
    public class FetchQuizQuestionsResponseDto
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Link { get; set; }
        public string[] Options { get; set; }
    }
}
