using Microsoft.AspNetCore.Mvc;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace VemboAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserPeriodProgressController : ControllerBase
    {
        private readonly IUserPeriodProgressService _service;

        public UserPeriodProgressController(IUserPeriodProgressService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value!;

            var result = await _service.GetAllUserPeriodProgress(userId);
            
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
        public async Task<IActionResult> GetByPeriodId(int periodId)
        {
            string userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value!;

            var progress = await _service.GetUserPeriodProgressByPeriodId(userId, periodId);

            return Ok(progress);
        }

        [Authorize]
        [HttpGet("WithMostXP/User/{userId}")]
        public async Task<IActionResult> GetWithMostXPByUserId(string userId)
        {
            var progress = await _service.GetUserPeriodProgressWithMostXPByUserId(userId);

            return Ok(progress);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserPeriodProgressDto dto)
        {
            var created = await _service.CreateUserPeriodProgress(dto);
            
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateUserPeriodProgressDto dto)
        {
            try
            {
                await _service.UpdateUserPeriodProgress(id, dto);
                
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
                await _service.DeleteUserPeriodProgress(id);
                
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
