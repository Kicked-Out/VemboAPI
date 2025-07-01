using Microsoft.AspNetCore.Mvc;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseService _exerciseService;

        public ExerciseController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var exercises = _exerciseService.GetAllExercise();
            return Ok(exercises);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var exercise = _exerciseService.GetExerciseById(id);
                return Ok(exercise);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] ExerciseDto exercise)
        {
            if (exercise == null || string.IsNullOrEmpty(exercise.Title))
            {
                return BadRequest("Invalid exercise data.");
            }

            try
            {
                var created = _exerciseService.CreateExercise(
                    exercise.Title,
                    exercise.LessonId,
                    exercise.Difficulty,
                    exercise.Order);

                return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ExerciseDto exercise)
        {
            if (exercise == null || string.IsNullOrEmpty(exercise.Title))
            {
                return BadRequest("Invalid exercise data.");
            }

            try
            {
                _exerciseService.UpdateExercise(
                    id,
                    exercise.Title,
                    exercise.LessonId,
                    exercise.Difficulty,
                    exercise.Order);

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
                _exerciseService.DeleteExercise(id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
