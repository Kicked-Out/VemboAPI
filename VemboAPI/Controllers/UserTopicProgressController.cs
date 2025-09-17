using Microsoft.AspNetCore.Mvc;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace VemboAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserTopicProgressController : ControllerBase
    {
        private readonly IUserTopicProgressService _service;

        public UserTopicProgressController(IUserTopicProgressService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            string userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value!;
            
            var result = await _service.GetAllUserTopicProgress(userId);
            
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
        [HttpGet("Period/{periodId}")]
        public async Task<IActionResult> GetAllByPeriodId(int periodId)
        {
            string userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value!;

            var result = await _service.GetAllUserTopicProgressByPeriodId(userId, periodId);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("Current/Period/{periodId}")]
        public async Task<IActionResult> GetCurrentByPeriodId(int periodId)
        {
            string userId = User.FindFirst("\"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value!;

            var result = await _service.GetCurrentUserTopicProgress(userId, periodId);

            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserTopicProgressDto dto)
        {
            var created = await _service.CreateUserTopicProgress(dto);
            
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateUserTopicProgressDto dto)
        {
            try
            {
                await _service.UpdateUserTopicProgress(id, dto);
                
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
                await _service.DeleteUserTopicProgress(id);
                
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
