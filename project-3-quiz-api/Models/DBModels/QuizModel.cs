﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace project_3_quiz_api.Models.DBModels
{
    public class QuizModel
    {
        // Primary Key
        [Key]
        public Guid Id { get; set; }

        // Properties
        [NotNull]
        public string Title { get; set; }
        [NotNull]
        public string NormalizedTitle { get; set; }
        public int TimeLimitMin { get; set; }

        [NotNull]
        public string Link { get; set; }

        [NotNull]
        public Guid UserId { get; set; }

        // Navigation Properties
        public virtual UserModel Users { get; set; }
        public IEnumerable<QuestionModel> Questions { get; set; }
        public IEnumerable<ScoreModel> Scores { get; set; }
    }
}
