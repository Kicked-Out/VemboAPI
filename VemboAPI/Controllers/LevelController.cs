using Microsoft.AspNetCore.Mvc;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace VemboAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LevelController : ControllerBase
    {
        private readonly ILevelService _levelService;

        public LevelController(ILevelService levelService)
        {
            _levelService = levelService;
        }
        [Authorize]

        [HttpGet]
        public IActionResult Get()
        {
            var levels = _levelService.GetAllLevels();
            return Ok(levels);
        }
        [Authorize]

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var level = _levelService.GetLevelById(id);
                return Ok(level);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]

        [HttpPost]
        public IActionResult Post([FromBody] Level level)
        {
            if (level == null || string.IsNullOrEmpty(level.Title))
            {
                return BadRequest("Invalid level data.");
            }

            try
            {
                var created = _levelService.CreateLevel(level.Title, level.UnitId, level.Order);
                return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Level level)
        {
            if (level == null || string.IsNullOrEmpty(level.Title))
            {
                return BadRequest("Invalid level data.");
            }

            try
            {
                _levelService.UpdateLevel(id, level.Title, level.UnitId, level.Order);
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
                _levelService.DeleteLevel(id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
