namespace project_3_quiz_api.Models.DTO
{
    public class FetchQuizByTitleResponseDto
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public int TimeLimitMin { get; set; }
    }
}
