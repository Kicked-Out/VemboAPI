using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VemboAPI.Domain.DTOs;
using VemboAPI.Infrastructure.Interfaces;
using System.Collections.Generic;

namespace VemboAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BadgeController : ControllerBase
    {
        private readonly IBadgeService _service;

        public BadgeController(IBadgeService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            var items = _service.GetAllBadges();
            return Ok(items);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var item = _service.GetBadgeById(id);
                return Ok(item);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Post([FromBody] CreateBadgeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = _service.CreateBadge(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateBadgeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _service.UpdateBadge(id, dto);
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
                _service.DeleteBadge(id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
