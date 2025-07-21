using Microsoft.AspNetCore.Mvc;
using VemboAPI.Infrastructure.Interfaces;

using VemboAPI.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Data;

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
            var result = _service.GetAllUserUnitProgress();
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
        [Authorize(Roles = "Admin")]
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
