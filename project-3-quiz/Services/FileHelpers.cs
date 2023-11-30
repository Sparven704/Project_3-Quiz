using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Http;

namespace project_3_quiz.Services
{
    public class FileHelpers
    {
        public static IFormFile ConvertToFormFile(IBrowserFile browserFile)
        {
            using (var stream = browserFile.OpenReadStream())
            {
                return new FormFile(stream, 0, stream.Length, null, Path.GetFileName(browserFile.Name))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = browserFile.ContentType
                };
            }
        }
    }
}
