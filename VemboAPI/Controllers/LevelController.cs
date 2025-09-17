using Microsoft.AspNetCore.Mvc;
using VemboAPI.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using VemboAPI.Domain.DTOs;

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
        public async Task<IActionResult> Get()
        {
            var levels = await _levelService.GetAllLevels();

            return Ok(levels);
        }

        [Authorize]
        [HttpGet("Unit/{unitId}")]
        public async Task<IActionResult> GetAllByUnitId(int unitId)
        {
            var levels = await _levelService.GetAllLevelsByUnitId(unitId);

            return Ok(levels);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var level = await _levelService.GetLevelById(id);
                
                return Ok(level);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateLevelDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var created = await _levelService.CreateLevel(dto);
                
                return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateLevelDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _levelService.UpdateLevel(id, dto);
                
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
                await _levelService.DeleteLevel(id);
                
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
