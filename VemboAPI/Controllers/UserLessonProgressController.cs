using Microsoft.AspNetCore.Mvc;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> GetAll()
        {
            string userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value!;

            var result = await _service.GetAllLessonProgress(userId);
            
            return Ok(result);
        }

        [Authorize]
        [HttpGet("Level/{levelId}")]
        public async Task<IActionResult> GetAll(int levelId)
        {
            string userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value!;

            var result = await _service.GetAllLessonProgressByLevelId(userId, levelId);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("Current/{levelId}")]
        public async Task<IActionResult> GetCurrent(int levelId)
        {
            string userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value!;

            var result = await _service.GetCurrentLessonProgressByLevelId(userId, levelId);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var ensured = await _service.EnsureProgressExists(userId, id);
            
            return Ok(ensured);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserLessonProgressDto dto)
        {
            var created = await _service.CreateLessonProgress(dto);
            
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateUserLessonProgressDto dto)
        {
            try
            {
                await _service.UpdateLessonProgress(id, dto);
                
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
                await _service.DeleteLessonProgress(id);
                
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
