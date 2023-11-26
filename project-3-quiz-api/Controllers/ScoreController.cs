using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using project_3_quiz_api.Models.DBModels;
using project_3_quiz_api.Models.DTO;
using project_3_quiz_api.Repositories.Interfaces;
using project_3_quiz_api.Repositories.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace project_3_quiz_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : Controller
    {
        private readonly ScoreRepository _scoreRepository;
        private readonly UserRepository _userRepository;

        public ScoreController(ScoreRepository scoreRepository, UserRepository userRepository)
        {
            _scoreRepository = scoreRepository;
            _userRepository = userRepository;
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateContent([FromBody] CreateScoreDto content)
        {
            try
            {
                if(content is null)
                    return BadRequest();

                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("rTZGBVT8KfHUgrVh5eZRwnc0zkz1eQmqK8hWDvNd")),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero // Optional: adjust for clock skew
                };

                ClaimsPrincipal principal;

                principal = tokenHandler.ValidateToken(content.JwtToken, validationParameters, out _);

                var testClaim = principal.FindFirst(ClaimTypes.Email);
                //Console.WriteLine(testClaim.Value);

                var user = await _userRepository.GetByConditionAsync(x => x.Email == testClaim.Value);
                var userId = user.Single().Id;

                ScoreModel newScore = new()
                {
                    Id = Guid.NewGuid(),
                    Score = content.Points,
                    UserId = userId,
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
                    return NotFound();

                List<LeaderboardScoreDTO> response = new();
                foreach (var score in leaderboard)
                {
                    response.Add(new LeaderboardScoreDTO()
                    {
                        Score = score.Score,
                        UserName = (await _userRepository.GetByConditionAsync(u => u.Id == score.UserId)).Single().Email
                    });
                }


                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return BadRequest();
        }

    }
}
