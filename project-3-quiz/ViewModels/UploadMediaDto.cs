﻿using System;
using Microsoft.AspNetCore.Http;
namespace project_3_quiz.ViewModels
{
    public class UploadMediaDto
    {
        public IFormFile File { get; set; }
        public string QuestionId { get; set; }
        
    }
}
