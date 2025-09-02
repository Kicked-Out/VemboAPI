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
    public class UserUnitProgressController : ControllerBase
    {
        private readonly IUserUnitProgressService _service;

        public UserUnitProgressController(IUserUnitProgressService service)
        {
            _service = service;
        }
        [Authorize]

        [HttpGet]
        public IActionResult Get()
        {
            string userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value!;

            var result = _service.GetAllUserUnitProgress(userId);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("Topic/{topicId}")]
        public IActionResult GetAllByTopicId(int topicId)
        {
            string userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value!;

            var result = _service.GetAllUserUnitProgressByTopicId(userId, topicId);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("Unit/{unitId}")]
        public IActionResult GetByUnitId(int unitId)
        {
            string userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value!;

            var result = _service.GetUserUnitProgressByUnitId(userId, unitId);

            return Ok(result);
        }
        [Authorize]
        [HttpGet("Current/Topic/{topicId}")]
        public IActionResult GetCurrent(int topicId)
        {
            string userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value!;

            var result = _service.GetCurrentUserUnitProgress(userId, topicId);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var progress = _service.GetUserUnitProgressById(id);
                return Ok(progress);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateUserUnitProgressDto dto)
        {
            var created = _service.CreateUserUnitProgress(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateUserUnitProgressDto dto)
        {
            try
            {
                _service.UpdateUserUnitProgress(id, dto);
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
                _service.DeleteUserUnitProgress(id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
