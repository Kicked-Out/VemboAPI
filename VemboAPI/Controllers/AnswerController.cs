using Microsoft.AspNetCore.Mvc;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.DTOs;

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

        [HttpGet]
        public IActionResult Get()
        {
            var answers = _answerService.GetAllAnswers();
            return Ok(answers);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var answer = _answerService.GetAnswerById(id);
                return Ok(answer);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] AnswerDto answer)
        {
            if (answer == null || string.IsNullOrEmpty(answer.Title))
            {
                return BadRequest("Invalid answer data.");
            }

            try
            {
                var created = _answerService.CreateAnswer(
                    answer.Title,
                    answer.isCorrect,
                    answer.QuestionId
                );

                return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] AnswerDto answer)
        {
            if (answer == null || string.IsNullOrEmpty(answer.Title))
            {
                return BadRequest("Invalid answer data.");
            }

            try
            {
                _answerService.UpdateAnswer(
                    id,
                    answer.Title,
                    answer.isCorrect,
                    answer.QuestionId
                );

                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _answerService.DeleteAnswer(id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
