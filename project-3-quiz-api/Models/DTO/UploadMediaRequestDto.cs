namespace project_3_quiz_api.Models.DTO
{
    public class UploadMediaRequestDto
    {
        public IFormFile File { get; set; }
        public string QuestionId { get; set; }
    }
}
