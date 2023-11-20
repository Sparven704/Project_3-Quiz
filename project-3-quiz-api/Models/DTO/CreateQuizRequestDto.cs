namespace project_3_quiz_api.Models.DTO
{
    public class CreateQuizRequestDto
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public int TimeLimitMin { get; set; }
    }
}
