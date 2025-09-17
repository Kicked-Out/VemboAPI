using Microsoft.AspNetCore.Mvc;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace VemboAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;

        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }
        
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var answers = await _answerService.GetAllAnswers();
            return Ok(answers);
        }

        [Authorize]
        [HttpGet("Question/{questionId}")]
        public async Task<IActionResult> GetByQuestionId(int questionId)
        {
            var answers = await _answerService.GetAllAnswersByQuestionId(questionId);

            return Ok(answers);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var answer = await _answerService.GetAnswerById(id);
                return Ok(answer);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateAnswerDto dto)
        {
            try
            {
                var created = await _answerService.CreateAnswer(dto);
                return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateAnswerDto dto)
        {
            try
            {
                await _answerService.UpdateAnswer(id, dto);
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
                await _answerService.DeleteAnswer(id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
