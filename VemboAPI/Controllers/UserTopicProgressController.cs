using Microsoft.AspNetCore.Mvc;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;

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
        public IActionResult GetAll()
        {
            string userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value!;
            
            var result = _service.GetAllUserTopicProgress(userId);
            return Ok(result);
        }
        [Authorize]

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var progress = _service.GetUserTopicProgressById(id);
                return Ok(progress);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("Period/{periodId}")]
        public IActionResult GetAllByPeriodId(int periodId)
        {
            string userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value!;

            var result = _service.GetAllUserTopicProgressByPeriodId(userId, periodId);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("Current/Period/{periodId}")]
        public IActionResult GetCurrentByPeriodId(int periodId)
        {
            string userId = User.FindFirst("\"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value!;

            var result = _service.GetCurrentUserTopicProgress(userId, periodId);

            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateUserTopicProgressDto dto)
        {
            var created = _service.CreateUserTopicProgress(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateUserTopicProgressDto dto)
        {
            try
            {
                _service.UpdateUserTopicProgress(id, dto);
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
                _service.DeleteUserTopicProgress(id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
