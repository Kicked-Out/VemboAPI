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
        public IActionResult Get()
        {
            var questions = _questionService.GetAllQuestions();
            return Ok(questions);
        }

        [Authorize]
        [HttpGet("Exercise/{exerciseId}")]
        public IActionResult GetByExcerciseId(int exerciseId)
        {
            var questions = _questionService.GetAllQuestionsByExcerciseId(exerciseId);

            return Ok(questions);
        }


        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var question = _questionService.GetQuestionById(id);
                return Ok(question);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateQuestionDto dto)
        {
            try
            {
                var created = _questionService.CreateQuestion(dto);
                return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateQuestionDto dto)
        {
            try
            {
                _questionService.UpdateQuestion(id, dto);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _questionService.DeleteQuestion(id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
