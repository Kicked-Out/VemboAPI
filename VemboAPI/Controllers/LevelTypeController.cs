using Microsoft.AspNetCore.Mvc;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace VemboAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LevelTypeController : ControllerBase
    {
        private readonly ILevelTypeService _service;

        public LevelTypeController(ILevelTypeService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var items = await _service.GetAll();
            
            return Ok(items);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var item = await _service.GetById(id);

                return Ok(item);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateLevelTypeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.Create(dto);
            
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateLevelTypeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _service.Update(id, dto);
                
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
                await _service.Delete(id);

                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
