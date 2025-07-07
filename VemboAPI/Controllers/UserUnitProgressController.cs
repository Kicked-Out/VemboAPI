using Microsoft.AspNetCore.Mvc;
using VemboAPI.Infrastructure.Interfaces;

using VemboAPI.Domain.DTOs;

namespace VemboAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserUnitProgressController : ControllerBase
    {
        private readonly IUserUnitProgressService _service;

        public UserUnitProgressController(IUserUnitProgressService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _service.GetAllUserUnitProgress();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var progress = _service.GetUserUnitProgressById(id);
                return Ok(progress);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserUnitProgressDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data.");

            var created = _service.CreateUserUnitProgress(dto.UserId, dto.UnitId, dto.isCompleted);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserUnitProgressDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data.");

            try
            {
                _service.UpdateUserUnitProgress(id, dto.UserId, dto.UnitId, dto.isCompleted);
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
                _service.DeleteUserUnitProgress(id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
