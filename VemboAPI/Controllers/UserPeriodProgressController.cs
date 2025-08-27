using Microsoft.AspNetCore.Mvc;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;

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
        public IActionResult Get()
        {
            string userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value!;

            var result = _service.GetAllUserPeriodProgress(userId);
            return Ok(result);
        }
        [Authorize]

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var progress = _service.GetUserPeriodProgressById(id);
                return Ok(progress);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("Period/{periodId}")]
        public IActionResult GetByPeriodId(int periodId)
        {
            string userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value!;

            var progress = _service.GetUserPeriodProgressByPeriodId(userId, periodId);

            return Ok(progress);
        }

        [Authorize]
        [HttpGet("WithMostXP/User/{userId}")]
        public IActionResult GetWithMostXPByUserId(string userId)
        {
            var progress = _service.GetUserPeriodProgressWithMostXPByUserId(userId);

            return Ok(progress);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateUserPeriodProgressDto dto)
        {
            var created = _service.CreateUserPeriodProgress(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateUserPeriodProgressDto dto)
        {
            try
            {
                _service.UpdateUserPeriodProgress(id, dto);
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
                _service.DeleteUserPeriodProgress(id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
