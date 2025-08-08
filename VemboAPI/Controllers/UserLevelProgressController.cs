using Microsoft.AspNetCore.Mvc;
using VemboAPI.Infrastructure.Interfaces;

using VemboAPI.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Security.Claims;

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
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var ensured = _service.EnsureProgressExists(userId, id);
            return Ok(ensured);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Post([FromBody] CreateUserLevelProgressDto dto)
        {
            var created = _service.CreateUserLevelProgress(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateUserLevelProgressDto dto)
        {
            try
            {
                _service.UpdateUserLevelProgress(id, dto);
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
