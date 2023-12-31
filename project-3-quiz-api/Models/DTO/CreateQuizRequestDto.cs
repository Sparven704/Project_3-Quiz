﻿using System.IdentityModel.Tokens.Jwt;

namespace project_3_quiz_api.Models.DTO
{
    public class CreateQuizRequestDto
    {
        public string JwtToken { get; set; }
        public string Title { get; set; }
        public int TimeLimitMin { get; set; }
    }
}
