using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VemboAPI.Domain.DTOs;
using VemboAPI.Infrastructure.Interfaces;

namespace VemboAPI.API.Controllers
{
    [ApiController]
    [Route("api/user-quest-progress")]
    public class UserQuestProgressController : ControllerBase
    {
        private readonly IUserQuestProgressService _userQuestProgressService;

        public UserQuestProgressController(IUserQuestProgressService userQuestProgressService)
        {
            _userQuestProgressService = userQuestProgressService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var items = await _userQuestProgressService.GetAllAsync();
            return Ok(items);
        }

        [Authorize]
        [HttpGet("{userId}/{questId}")]
        public async Task<IActionResult> Get(string userId, int questId)
        {
            try
            {
                var item = await _userQuestProgressService.GetByIdsAsync(userId, questId);
                return Ok(item);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserQuestProgressDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var created = await _userQuestProgressService.CreateAsync(dto);
                return CreatedAtAction(nameof(Get), new { userId = created.UserId, questId = created.QuestId }, created);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("{userId}/{questId}")]
        public async Task<IActionResult> Put(string userId, int questId, [FromBody] UpdateUserQuestProgressDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _userQuestProgressService.UpdateAsync(userId, questId, dto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("{userId}/{questId}")]
        public async Task<IActionResult> Delete(string userId, int questId)
        {
            try
            {
                await _userQuestProgressService.DeleteAsync(userId, questId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
