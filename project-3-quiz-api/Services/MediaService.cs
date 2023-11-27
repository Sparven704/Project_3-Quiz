using project_3_quiz_api.Data;
using project_3_quiz_api.Models.DBModels;
using project_3_quiz_api.Repositories.Repository;

namespace project_3_quiz_api.Services
{
    public class MediaService : IMediaService
    {
        private readonly MediaRepository _mediaRepository;

        public MediaService(MediaRepository mediaRepository)
        {
            _mediaRepository = mediaRepository;
        }

        public async Task<MediaModel> GetMediaByIdAsync(Guid mediaId)
        {
            var mediaQuerry = await _mediaRepository.GetByConditionAsync(x => x.Id == mediaId);
            var media = mediaQuerry.Single();

            return media;
        }

        public async Task<MediaModel> UploadMediaAsync(IFormFile file, Guid questionId)
        {
            var mediaId = Guid.NewGuid();
            var fileName = $"{mediaId}{Path.GetExtension(file.FileName)}";
            var filePath = await SaveFileAsync(file, fileName);

            var media = new MediaModel
            {
                Id = mediaId,
                MediaType = file.ContentType,
                MediaUrl = filePath,
                QuestionId = questionId
            };

            await _mediaRepository.CreateAsync(media);
            await _mediaRepository.SaveAsync();

            return media;
        }

        private async Task<string> SaveFileAsync(IFormFile file, string fileName)
        {
            string imageDir = "wwwroot/images";
            string videoDir = "wwwroot/videos";
            string destDir = imageDir;


            if (file.ContentType.StartsWith("video"))
            {
                bool videoDirExist = Directory.Exists(videoDir);

                if (!videoDirExist)
                {
                    Directory.CreateDirectory(videoDir);
                }

                destDir = videoDir;

            }
            else if (file.ContentType.StartsWith("image"))
            {
                bool imageDirExist = Directory.Exists(imageDir);

                if (!imageDirExist)
                {
                    Directory.CreateDirectory(imageDir);
                }

            }

            Directory.CreateDirectory(fileName);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), destDir, fileName);
            // Save the file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            destDir = destDir.Replace("wwwroot/", "");
            var returnPath = Path.Combine(destDir, fileName);

            returnPath = returnPath.Replace("\\", "/");


            return returnPath;
        }
    }
}
