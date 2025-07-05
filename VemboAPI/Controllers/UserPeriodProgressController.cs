using Microsoft.AspNetCore.Mvc;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.DTOs;

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

        [HttpGet]
        public IActionResult Get()
        {
            var result = _service.GetAllUserPeriodProgress();
            return Ok(result);
        }

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

        [HttpPost]
        public IActionResult Post([FromBody] UserPeriodProgressDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data.");

            var created = _service.CreateUserPeriodProgress(dto.UserId, dto.PeriodId, dto.isCompleted);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserPeriodProgressDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data.");

            try
            {
                _service.UpdateUserPeriodProgress(id, dto.UserId, dto.PeriodId, dto.isCompleted);
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
