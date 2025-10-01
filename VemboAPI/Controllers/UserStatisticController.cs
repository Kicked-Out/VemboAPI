using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using VemboAPI.Domain.DTOs;
using VemboAPI.Infrastructure.Interfaces;
using System.Security.Claims;
using VemboAPI.Domain.DTO;

namespace VemboAPI.API.Controllers
{
    [ApiController]
    [Route("api/user-statistics")]
    public class UserStatisticController : ControllerBase
    {
        private readonly IUserStatisticService _service;

        public UserStatisticController(IUserStatisticService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [Authorize]
        [HttpGet("User/{userId}")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            try
            {
                var result = await _service.GetByUserId(userId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try { return Ok(await _service.GetByIdAsync(id)); }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
        }

        [Authorize]
        [HttpGet("by-user")]
        public async Task<IActionResult> GetByUser()
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            try
            {
                var result = await _service.GetByUserIdAsync(userIdStr);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserStatisticDto dto)
        {
            try
            {
                var created = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
            }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserStatisticDto dto)
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

                await _service.UpdateTotalXP(userId, dto);

                return Ok();
            } catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("Current/Coins")]
        public async Task<IActionResult> UpdateCoins([FromBody] UpdateUserCoinsDto dto)
        {
            try
            {
                string userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value!;

                await _service.UpdateCoins(userId, dto);

                return Ok();
            } catch(KeyNotFoundException ex)
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
