namespace project_3_quiz_api.Models.DTO
{
    public class FetchQuizByUserIdResponseDto
    {
        public string Title { get; set; }
        public int TimeLimitMin { get; set; }
        public string Link { get; set; }
    }
}
