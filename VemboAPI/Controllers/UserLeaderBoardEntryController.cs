using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using VemboAPI.Domain.DTOs;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.DTO;

namespace VemboAPI.API.Controllers
{
    [ApiController]
    [Route("api/admin/leaderboard")]
    public class UserLeaderBoardController : ControllerBase
    {
        private readonly IUserLeaderBoardService _service;

        public UserLeaderBoardController(IUserLeaderBoardService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try { return Ok(await _service.GetByIdAsync(id)); }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserLeaderBoardEntryDto dto)
        {
            try
            {
                var created = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
            }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserLeaderBoardEntryDto dto)
        {
            try
            {
                await _service.UpdateAsync(id, dto);
                return Ok();
            }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
        }

        [Authorize]
        [HttpPut("Current/TotalXP")]
        public async Task<IActionResult> UpdateTotalXP([FromBody] UpdateUserTotalXPDto dto)
        {
            try
            {
                string userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value!;

                await _service.UpdateTotalXPAsync(userId, dto);

                return Ok();
            } catch (KeyNotFoundException ex)
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
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
        }
    }

}

