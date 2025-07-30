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
    public class UserLessonProgressController : ControllerBase
    {
        private readonly IUserLessonProgressService _service;

        public UserLessonProgressController(IUserLessonProgressService service)
        {
            _service = service;
        }
        [Authorize]

        [HttpGet]
        public IActionResult Get()
        {
            var result = _service.GetAllLessonProgress();
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
        public IActionResult Post([FromBody] CreateUserLessonProgressDto dto)
        {
            var created = _service.CreateLessonProgress(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateUserLessonProgressDto dto)
        {
            try
            {
                _service.UpdateLessonProgress(id, dto);
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
                _service.DeleteLessonProgress(id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
