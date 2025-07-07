using Microsoft.AspNetCore.Mvc;
using VemboAPI.Infrastructure.Interfaces;

using VemboAPI.Domain.DTOs;

namespace VemboAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserLevelProgressController : ControllerBase
    {
        private readonly IUserLevelProgressService _service;

        public UserLevelProgressController(IUserLevelProgressService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _service.GetAllUserLevelProgress();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var progress = _service.GetUserLevelProgressById(id);
                return Ok(progress);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserLevelProgressDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data.");

            var created = _service.CreateUserLevelProgress(dto.UserId, dto.LevelId, dto.isCompleted);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserLevelProgressDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data.");

            try
            {
                _service.UpdateUserLevelProgress(id, dto.UserId, dto.LevelId, dto.isCompleted);
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
                _service.DeleteUserLevelProgress(id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
