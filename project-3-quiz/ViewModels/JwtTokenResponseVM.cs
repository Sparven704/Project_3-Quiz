using System.Text.Json.Serialization;

namespace project_3_quiz.ViewModels
{
    public class JwtTokenResponseVM
    {
        [JsonPropertyName("AccessToken")]
        public string AccessToken { get; set; }
    }
}
