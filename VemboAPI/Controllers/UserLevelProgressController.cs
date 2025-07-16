using Microsoft.AspNetCore.Mvc;
using VemboAPI.Infrastructure.Interfaces;

using VemboAPI.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Data;

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
        [Authorize]

        [HttpGet]
        public IActionResult Get()
        {
            var result = _service.GetAllUserLevelProgress();
            return Ok(result);
        }
        [Authorize]

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
        [Authorize(Roles = "Admin")]

        [HttpPost]
        public IActionResult Post([FromBody] UserLevelProgressDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data.");

            var created = _service.CreateUserLevelProgress(dto.UserId, dto.LevelId, dto.isCompleted);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }
        [Authorize(Roles = "Admin")]

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
        [Authorize(Roles = "Admin")]

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
