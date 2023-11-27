using project_3_quiz_api.Models.DBModels;

namespace project_3_quiz_api.Services
{
    public interface IMediaService
    {
        Task<MediaModel> GetMediaByIdAsync(Guid mediaId);
        Task<MediaModel> UploadMediaAsync(IFormFile file, Guid questionId);
    }
}
