using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using project_3_quiz_api.Models.DBModels;
using project_3_quiz_api.Models.DTO;
using project_3_quiz_api.Repositories.Interfaces;
using project_3_quiz_api.Repositories.Repository;

namespace project_3_quiz_api.Controllers
{
    public class ScoreController : Controller
    {
        private readonly ScoreRepository _scoreRepository;

        public ScoreController(ScoreRepository scoreRepository)
        {
            _scoreRepository = scoreRepository;
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateContent([FromBody] CreateScoreDto content)
        {

            try
            {
                ScoreModel newScore = new()
                {
                    Id = Guid.NewGuid(),
                    Score = content.Points,
                    UserId = content.UserId,
                    QuizId = content.QuizId,
                };

                await _scoreRepository.CreateAsync(newScore);
                await _scoreRepository.SaveAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return BadRequest();
        }

        // Get leaderboard for quiz
        [HttpGet("{quizid}")]
        public async Task<IActionResult> FetchLeaderboard(Guid quizid)
        {
            try
            {
                var query = await _scoreRepository.GetByConditionAsync(s => s.QuizId == quizid);
                if (query is null)
                    return NotFound();

                List<ScoreModel>? leaderboard = query.OrderByDescending(x => x.Score).ToList();

                if (leaderboard.IsNullOrEmpty())
                    return StatusCode(500);


                return Ok(leaderboard);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return BadRequest();
        }

    }
}
