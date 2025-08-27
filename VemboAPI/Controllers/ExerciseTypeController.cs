using Microsoft.AspNetCore.Mvc;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Data;

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
        public IActionResult Get()
        {
            var types = _exerciseTypeService.GetAllExerciseTypes();
            return Ok(types);
        }
        [Authorize]

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
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateExerciseTypeDto dto)
        {
            var created = _exerciseTypeService.CreateExerciseType(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateExerciseTypeDto dto)
        {
            try
            {
                _exerciseTypeService.UpdateExerciseType(id, dto);
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
