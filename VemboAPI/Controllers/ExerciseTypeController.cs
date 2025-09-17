using Microsoft.AspNetCore.Mvc;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var types = await _exerciseTypeService.GetAllExerciseTypes();
            
            return Ok(types);
        }
        [Authorize]

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var type = await _exerciseTypeService.GetExerciseTypeById(id);
                
                return Ok(type);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateExerciseTypeDto dto)
        {
            var created = await _exerciseTypeService.CreateExerciseType(dto);

            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateExerciseTypeDto dto)
        {
            try
            {
                await _exerciseTypeService.UpdateExerciseType(id, dto);
                
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
                await _exerciseTypeService.DeleteExerciseType(id);

                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
