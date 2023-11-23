using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using project_3_quiz_api.Models.DBModels;
using project_3_quiz_api.Models.DTO;
using project_3_quiz_api.Repositories.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace project_3_quiz_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly QuizRepository _quizRepository;
        private readonly UserRepository _userRepository;

        public QuizController(QuizRepository quizRepository, UserRepository userRepository)
        {
            _quizRepository = quizRepository;
            _userRepository = userRepository;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateQuiz([FromBody] CreateQuizRequestDto requestDto)
        {
            try
            {

                if (requestDto == null)
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
                
                principal = tokenHandler.ValidateToken(requestDto.JwtToken, validationParameters, out _);

                var testClaim = principal.FindFirst(ClaimTypes.Email);
                // Console.WriteLine(testClaim.Value);

                var user = await _userRepository.GetByConditionAsync(x => x.Email == testClaim.Value);
                var userId = user.Single().Id;


                QuizModel newQuiz = new()
                {
                    Id = Guid.NewGuid(),
                    Title = requestDto.Title,
                    TimeLimitMin = requestDto.TimeLimitMin,
                    Link = Guid.NewGuid().ToString(),
                    UserId = userId,
                    NormalizedTitle = requestDto.Title.ToUpper()
                };

                await _quizRepository.CreateAsync(newQuiz);
                await _quizRepository.SaveAsync();

                var response = new
                {
                    QuizId = newQuiz.Id,
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return BadRequest();
        }

        // Gets a quiz by Link
        [HttpPost("link/{link}")]
        public async Task<IActionResult> FetchQuiz(string link)
        {
            try
            {
                var querry = await _quizRepository.GetByConditionAsync(q => q.Link == link);
                if (querry is null)
                    return NotFound();

                QuizModel? quiz = querry.Single();
                if (quiz is null)
                    return StatusCode(500); // Should never happen

                var responseDto = new FetchQuizResponseDto()
                {
                    Title = quiz.Title,
                    TimeLimitMin = quiz.TimeLimitMin,
                    QuizId = quiz.Id
                };

                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return BadRequest();
        }

        // Fetch quizzes by userId
        [HttpPost("user/{userId}")]
        public async Task<IActionResult> FetchQuizByUserId(Guid userId)
        {
            try
            {
                var querry = await _quizRepository.GetByConditionAsync(q => q.UserId == userId);
                if (querry is null)
                    return NotFound();

                List<QuizModel> userQuizzes = querry.ToList();
                if (userQuizzes.IsNullOrEmpty())
                    return NotFound();

                List<FetchQuizByUserIdResponseDto> response = new();
                foreach (var quiz in userQuizzes)
                {
                    response.Add(new FetchQuizByUserIdResponseDto()
                    {
                        Title = quiz.Title,
                        TimeLimitMin = quiz.TimeLimitMin,
                        Link = quiz.Link
                    });
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return BadRequest();
        }

        // Fetch all quizz titles and links
        [HttpGet("all")]
        public async Task<IActionResult> FetchAllQuizzes()
        {
            try
            {
                var querry = await _quizRepository.GetAllAsync();
                if (querry is null)
                    return NotFound();

                List<QuizModel> quizzes = querry.ToList();
                if (quizzes.IsNullOrEmpty())
                    return NotFound();

                List<FetchAllQuizzesResponseDto> fetchAllQuizzesResponseDto = new();
                foreach (var quiz in quizzes)
                {
                    fetchAllQuizzesResponseDto.Add(new FetchAllQuizzesResponseDto()
                    {
                        Title = quiz.Title,
                        Link = quiz.Link
                    });
                }

                return Ok(fetchAllQuizzesResponseDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return BadRequest();
        }

        [HttpPost("title/{title}")]
        public async Task<IActionResult> FetchQuizByTitle(string title)
        {
            // Change to return a list insted of single since there can be more than one quiz with the same name
            try
            {
                var querry = await _quizRepository.GetByConditionAsync(q => q.NormalizedTitle == title.ToUpper());
                if (querry is null)
                    return NotFound();

                var quiz = querry.Single();
                if (quiz is null)
                    return StatusCode(500); // Should never happen

                var responseDto = new FetchQuizByTitleResponseDto
                {
                    Title = quiz.Title,
                    Link = quiz.Link,
                    TimeLimitMin = quiz.TimeLimitMin
                };

                return Ok(responseDto);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return BadRequest();
        }

        // Delete quiz by Id 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuiz(Guid id)
        {
            // add delete questions
            try
            {
                var querry = await _quizRepository.GetByConditionAsync(q => q.Id == id);
                if (querry is null)
                    return NotFound();

                var quizToDelete = querry.Single();
                if (quizToDelete is null)
                    return StatusCode(500); // Should never happen

                _quizRepository.Delete(quizToDelete);
                await _quizRepository.SaveAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return BadRequest();
        }

        

    }
}
