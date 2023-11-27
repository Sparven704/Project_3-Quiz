using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project_3_quiz_api.Models.DTO;
using project_3_quiz_api.Services;

namespace project_3_quiz_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly MediaService _mediaService;

        public MediaController(MediaService mediaService)
        {
            _mediaService = mediaService;
        }

        [HttpGet("{guid}")]
        public async Task<IActionResult> GetMedia(Guid guid)
        {

            var media = await _mediaService.GetMediaByIdAsync(guid);

            FetchMediaResponseDto fetchMediaResponseDto = new()
            {
                Type = media.MediaType,
                Path = media.MediaUrl
            };

            return Ok(fetchMediaResponseDto);
        }

        [HttpPost]
        public async Task<IActionResult> UploadMediaAsync([FromBody] UploadMediaRequestDto requestDto)
        {
            //Upload video or image

            int maxMb = 13;
            long megaByte = 1024 * 1024;

            long maxAllowedSizeInBytes = maxMb * megaByte;
            string[] permittedFileTypes = { ".jpg", ".jpeg", ".png", ".gif", ".mp4" };
            var extension = Path.GetExtension(requestDto.File.FileName).ToLowerInvariant();

            if (requestDto.File.Length > maxAllowedSizeInBytes)
            {
                return BadRequest("File size exceeds the allowable limit.");
            }

            if (string.IsNullOrEmpty(extension) || !permittedFileTypes.Contains(extension))
            {
                return BadRequest("Invalid file type. Submitted filetype: " + requestDto.File.ContentType);
            }


            var newMedia = await _mediaService.UploadMediaAsync(requestDto.File, requestDto.QuestionId);

            return Ok(newMedia);
        }
    }
}
