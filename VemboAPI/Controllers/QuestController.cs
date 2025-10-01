using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VemboAPI.Domain.DTOs;
using VemboAPI.Infrastructure.Interfaces;

namespace VemboAPI.API.Controllers
{
    [ApiController]
    [Route("api/quests")]
    public class QuestController : ControllerBase
    {
        private readonly IQuestService _questService;

        public QuestController(IQuestService questService)
        {
            _questService = questService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var quests = await _questService.GetAllAsync();
            return Ok(quests);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var quest = await _questService.GetByIdAsync(id);
                return Ok(quest);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("monthly")]

        [Authorize]
        [HttpGet("Current/Monthly")]
        public async Task<IActionResult> GetCurrentMonthly()
        {
            try
            {
                var quest = await _questService.GetCurrentMonthly();

                return Ok(quest);
            } catch (KeyNotFoundException ex)
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpGet("Current/Daily")]
        public async Task<IActionResult> GetCurrentDaily()
        {
            try
            {
                var quests = await _questService.GetCurrentDaily();

                return Ok(quests);
            } catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateQuestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var created = await _questService.CreateAsync(dto);
                return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateQuestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                await _questService.UpdateAsync(id, dto);
                return Ok();
            }
            catch (KeyNotFoundException ex)
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
                await _questService.DeleteAsync(id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
