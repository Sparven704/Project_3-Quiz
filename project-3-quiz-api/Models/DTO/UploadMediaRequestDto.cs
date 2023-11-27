namespace project_3_quiz_api.Models.DTO
{
    public class UploadMediaRequestDto
    {
        public IFormFile File { get; set; }
        public Guid QuestionId { get; set; }
    }
}
