namespace project_3_quiz_api.Models.DTO
{
    public class CreateScoreDto
    {
        public Guid QuizId { get; set; }
        public Guid UserId { get; set; }
        public int Points { get; set; }

    }
}
