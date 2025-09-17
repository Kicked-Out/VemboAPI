using Microsoft.AspNetCore.Mvc;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value!;

            var result = await _service.GetAllUserUnitProgress(userId);
            
            return Ok(result);
        }

        [Authorize]
        [HttpGet("Topic/{topicId}")]
        public async Task<IActionResult> GetAllByTopicId(int topicId)
        {
            string userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value!;

            var result = await _service.GetAllUserUnitProgressByTopicId(userId, topicId);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("Unit/{unitId}")]
        public async Task<IActionResult> GetByUnitId(int unitId)
        {
            string userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value!;

            var result = await _service.GetUserUnitProgressByUnitId(userId, unitId);

            return Ok(result);
        }
        [Authorize]
        [HttpGet("Current/Topic/{topicId}")]
        public async Task<IActionResult> GetCurrent(int topicId)
        {
            string userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value!;

            var result = await _service.GetCurrentUserUnitProgress(userId, topicId);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var progress = await _service.GetUserUnitProgressById(id);
                
                return Ok(progress);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserUnitProgressDto dto)
        {
            var created = await _service.CreateUserUnitProgress(dto);
            
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateUserUnitProgressDto dto)
        {
            try
            {
                await _service.UpdateUserUnitProgress(id, dto);
                
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
                await _service.DeleteUserUnitProgress(id);
                
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
