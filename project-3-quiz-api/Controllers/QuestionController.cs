﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using project_3_quiz_api.Models.DBModels;
using project_3_quiz_api.Models.DTO;
using project_3_quiz_api.Repositories.Repository;
using project_3_quiz_api.Services;

namespace project_3_quiz_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly QuestionRepository _questionRepository;
        private readonly OptionRepository _optionRepository;

        public QuestionController(QuestionRepository questionRepository, OptionRepository optionRepository)
        {
            _questionRepository = questionRepository;
            _optionRepository = optionRepository;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionRequestDto requestDto)
        {
            try
            {
                if (requestDto == null)
                    return BadRequest();

                QuestionModel newQuestion = new()
                {
                    Id = Guid.NewGuid(),
                    Question = requestDto.Question,
                    Answer = requestDto.Answer,
                    HasMedia = requestDto.HasMedia,
                    QuizId = requestDto.QuizId,
                    IsMultipleAnswer = requestDto.IsMultipleAnswer,
                    MediaId = requestDto.MediaId
                };

                if (newQuestion.MediaId == Guid.Empty)
                {
                    newQuestion.MediaId = null;
                }

                await _questionRepository.CreateAsync(newQuestion);
                //await Console.Out.WriteLineAsync("test");
                if (newQuestion.IsMultipleAnswer is true)
                {
                    foreach (var option in requestDto.Options)
                    {
                        OptionModel newOption = new()
                        {
                            Id = Guid.NewGuid(),
                            Text = option,
                            QuestionId = newQuestion.Id
                        };
                        await _optionRepository.CreateAsync(newOption);
                    }
                }
                
                await _questionRepository.SaveAsync();

                return Ok(newQuestion.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return BadRequest();
        }

        // Fetch questions by quizId
        [HttpGet("{quizId}")]
        public async Task<IActionResult> FetchQuizQuestions(Guid quizId)
        {
            try
            {
                var querry = await _questionRepository.GetByConditionAsync(q => q.QuizId == quizId);
                if (querry is null)
                    return NotFound();

                List<QuestionModel>? querryResult = querry.ToList();
                if (querryResult.IsNullOrEmpty())
                    return NotFound();

                List<FetchQuizQuestionsResponseDto> fetchQuizQuestionsResponseDto = new();
                foreach (var question in querryResult)
                {
                    if (question.IsMultipleAnswer is true)
                    {
                        var optionQuerry = await _optionRepository.GetByConditionAsync(o => o.QuestionId == question.Id);
                        if (optionQuerry is null)
                            return NotFound();

                        List<OptionModel> optionQuerryResult = optionQuerry.ToList();
                        if (optionQuerryResult.IsNullOrEmpty())
                            return NotFound();

                        fetchQuizQuestionsResponseDto.Add(new FetchQuizQuestionsResponseDto()
                        {
                            Question = question.Question,
                            Answer = question.Answer,
                            HasMedia = question.HasMedia,
                            Options = optionQuerryResult.Select(o => o.Text).ToArray(),
                            MediaId = question.MediaId

                        });
                    }
                    else
                    {
                        fetchQuizQuestionsResponseDto.Add(new FetchQuizQuestionsResponseDto()
                        {
                            Question = question.Question,
                            Answer = question.Answer,
                            HasMedia = question.HasMedia,
                            Options = null,
                            MediaId = question.MediaId
                        });
                    }
                }

                return Ok(fetchQuizQuestionsResponseDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return BadRequest();
        }

        // Delete Question & corresponding options
        [HttpDelete("{questionId}")]
        public async Task<IActionResult> DeleteQuestion(Guid questionId)
        {
            try
            {
                var querry = await _questionRepository.GetByConditionAsync(x => x.Id == questionId);
                if (querry is null)
                    return NotFound();

                var questionToDelete = querry.Single();
                if (questionToDelete is null)
                    return StatusCode(500); // Should never happen

                var optionsQuerry = await _optionRepository.GetByConditionAsync(x => x.QuestionId == questionId);
                if (optionsQuerry is null)
                    return BadRequest();

                var optionsToDelete = optionsQuerry.ToArray();
                foreach (var option in optionsToDelete)
                {
                    if (option is not null)
                    {
                        _optionRepository.Delete(option);
                    }
                }
                _questionRepository.Delete(questionToDelete);

                await _questionRepository.SaveAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return BadRequest();
        }

    }
}
