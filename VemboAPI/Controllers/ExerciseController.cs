using Microsoft.AspNetCore.Mvc;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var exercises = await _exerciseService.GetAllExercise();
            
            return Ok(exercises);
        }

        [Authorize]
        [HttpGet("Lesson/{lessonId}")]
        public async Task<IActionResult> GetByLessonId(int lessonId)
        {
            var exercises = await _exerciseService.GetAllExerciseByLessonId(lessonId);

            return Ok(exercises);
        }
        
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var exercise = await _exerciseService.GetExerciseById(id);
                
                return Ok(exercise);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateExerciseDto dto)
        {
            try
            {
                var created = await _exerciseService.CreateExercise(dto);
                
                return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateExerciseDto dto)
        {
            try
            {
                await _exerciseService.UpdateExercise(id, dto);
                
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
                await _exerciseService.DeleteExercise(id);
                
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
