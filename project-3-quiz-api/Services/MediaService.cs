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

        public async Task<MediaModel> UploadMediaAsync(IFormFile file)
        {
            var mediaId = Guid.NewGuid();
            var fileName = $"{mediaId}{Path.GetExtension(file.FileName)}";
            var filePath = await SaveFileAsync(file, fileName);

            var media = new MediaModel
            {
                Id = mediaId,
                MediaType = file.ContentType,
                MediaUrl = filePath
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
            string[] permittedImageFileTypes = { "jpg", "jpeg", "png" };
            string[] permittedVideoFileTypes = { "mp4", "gif", "mov" };

            //Replace Server directory with Client directory
            string currentDir = Directory.GetCurrentDirectory();
            currentDir = currentDir.Replace("project-3-quiz-api", "project-3-quiz");

            string fileExtension = Path.GetExtension(file.FileName)?.TrimStart('.');

            if (!string.IsNullOrEmpty(fileExtension))
            {
                if (permittedVideoFileTypes.Contains(fileExtension, StringComparer.OrdinalIgnoreCase))
                {
                    string directory = Path.Combine(currentDir, videoDir);
                    bool videoDirExist = Directory.Exists(directory);

                    if (!videoDirExist)
                    {
                        Directory.CreateDirectory(directory);
                    }

                    destDir = videoDir;
                }
                else if (permittedImageFileTypes.Contains(fileExtension, StringComparer.OrdinalIgnoreCase))
                {
                    string directory = Path.Combine(currentDir, imageDir);
                    bool imageDirExist = Directory.Exists(directory);

                    if (!imageDirExist)
                    {
                        Directory.CreateDirectory(directory);
                    }
                }
            }
            


            var filePath = Path.Combine(currentDir, destDir, fileName);

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
