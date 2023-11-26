namespace project_3_quiz_api.Models.DTO
{
    public class FetchAllQuizzesResponseDto
    {
        public string Title { get; set; }
        public Guid Id { get; set; }
        public int TimeLimitMin { get; set; }

    }
}
