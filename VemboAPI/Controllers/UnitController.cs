using Microsoft.AspNetCore.Mvc;
using VemboAPI.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnitController : ControllerBase
    {
        private readonly IUnitService _unitService;

        public UnitController(IUnitService unitService)
        {
            _unitService = unitService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var units = await _unitService.GetAllUnits();
            
            return Ok(units);
        }

        [Authorize]
        [HttpGet("Topic/{topicId}")]
        public async Task<IActionResult> GetAllByTopicId(int topicId)
        {
            var units = await _unitService.GetAllUnitsByTopicId(topicId);

            return Ok(units);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var unit = await _unitService.GetUnitById(id);

                return Ok(unit);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUnitDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _unitService.CreateUnit(dto);

            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUnitDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _unitService.UpdateUnit(id, dto);
                
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
                await _unitService.DeleteUnit(id);
                
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
