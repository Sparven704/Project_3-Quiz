using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using project_3_quiz_api.Models.DBModels;
using project_3_quiz_api.Models.DTO;
using project_3_quiz_api.Repositories.Repository;

namespace project_3_quiz_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly QuizRepository _quizRepository;

        public QuizController(QuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateQuiz([FromBody] CreateQuizRequestDto requestDto)
        {
            try
            {
                if (requestDto == null)
                    return BadRequest();

                QuizModel newQuiz = new()
                {
                    Id = Guid.NewGuid(),
                    Title = requestDto.Title,
                    TimeLimitMin = requestDto.TimeLimitMin,
                    Link = Guid.NewGuid().ToString(),
                    UserId = requestDto.UserId
                };

                await _quizRepository.CreateAsync(newQuiz);
                await _quizRepository.SaveAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return BadRequest();
        }

        // Gets a quiz by Link
        [HttpPost("{link}")]
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


                return Ok(quiz);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
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
