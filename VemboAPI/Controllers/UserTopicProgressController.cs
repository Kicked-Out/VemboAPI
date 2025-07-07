using Microsoft.AspNetCore.Mvc;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.DTOs;

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

        [HttpGet]
        public IActionResult Get()
        {
            var result = _service.GetAllUserTopicProgress();
            return Ok(result);
        }

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

        [HttpPost]
        public IActionResult Post([FromBody] UserTopicProgressDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data.");

            var created = _service.CreateUserTopicProgress(dto.UserId, dto.TopicId, dto.isCompleted);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserTopicProgressDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data.");

            try
            {
                _service.UpdateUserTopicProgress(id, dto.UserId, dto.TopicId, dto.isCompleted);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

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
