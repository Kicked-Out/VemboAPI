using Microsoft.AspNetCore.Mvc;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExerciseTypeController : ControllerBase
    {
        private readonly IExerciseTypeService _exerciseTypeService;

        public ExerciseTypeController(IExerciseTypeService exerciseTypeService)
        {
            _exerciseTypeService = exerciseTypeService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var types = _exerciseTypeService.GetAllExerciseTypes();
            return Ok(types);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var type = _exerciseTypeService.GetExerciseTypeById(id);
                return Ok(type);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] ExerciseTypeDto type)
        {
            if (type == null || string.IsNullOrEmpty(type.Title))
            {
                return BadRequest("Invalid exercise type data.");
            }

            var created = _exerciseTypeService.CreateExerciseType(type.Title);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ExerciseTypeDto type)
        {
            if (type == null || string.IsNullOrEmpty(type.Title))
            {
                return BadRequest("Invalid exercise type data.");
            }

            try
            {
                _exerciseTypeService.UpdateExerciseType(id, type.Title);
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
                _exerciseTypeService.DeleteExerciseType(id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
