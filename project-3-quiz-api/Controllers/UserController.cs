using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using project_3_quiz_api.Models.DTO;
using project_3_quiz_api.Repositories.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace project_3_quiz_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly ScoreRepository _scoreRepository;
        private readonly QuizRepository _quizRepository;

        public UserController(UserRepository userRepository, ScoreRepository scoreRepository, QuizRepository quizRepository)
        {
            _userRepository = userRepository;
            _scoreRepository = scoreRepository;
            _quizRepository = quizRepository;
        }
        // return all quizzes for a user, 
        [HttpPost("profile")]
        public async Task<IActionResult> FetchProfile([FromBody] string jwtToken)
        {
            try
            {
                if (jwtToken == null)
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

                principal = tokenHandler.ValidateToken(jwtToken, validationParameters, out _);

                var testClaim = principal.FindFirst(ClaimTypes.Email);
                //Console.WriteLine(testClaim.Value);

                var user = await _userRepository.GetByConditionAsync(x => x.Email == testClaim.Value);
                var userId = user.Single().Id;

                var userQuizzesQuerry = await _quizRepository.GetByConditionAsync(x => x.UserId == userId);
                var userQuizzes = userQuizzesQuerry.ToArray();

                FetchProfileResponseDto response = new();
                response.Email = testClaim.Value;
                response.QuizTitle = userQuizzes.Select(x => x.Title).ToArray();
                response.QuizLink = userQuizzes.Select(x => x.Link).ToArray();

                return Ok(response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500);

            }

        }

    }
}
