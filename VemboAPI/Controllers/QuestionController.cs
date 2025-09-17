using Microsoft.AspNetCore.Mvc;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace VemboAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var questions = await _questionService.GetAllQuestions();
            return Ok(questions);
        }

        [Authorize]
        [HttpGet("Exercise/{exerciseId}")]
        public async Task<IActionResult> GetByExcerciseId(int exerciseId)
        {
            var questions = await _questionService.GetAllQuestionsByExcerciseId(exerciseId);

            return Ok(questions);
        }


        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var question = await _questionService.GetQuestionById(id);
                return Ok(question);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateQuestionDto dto)
        {
            try
            {
                var created = await _questionService.CreateQuestion(dto);
                return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateQuestionDto dto)
        {
            try
            {
                await _questionService.UpdateQuestion(id, dto);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _questionService.DeleteQuestion(id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
