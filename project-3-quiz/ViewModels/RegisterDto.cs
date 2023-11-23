using System.ComponentModel.DataAnnotations;

namespace project_3_quiz.ViewModels
{
    public class RegisterDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string[] Roles { get; set; }
    }
}
