using Microsoft.AspNetCore.Mvc;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _lessonService;

        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var lessons = await _lessonService.GetAllLessons();
            
            return Ok(lessons);
        }

        [HttpGet("Level/{levelId}")]
        public async Task<IActionResult> GetByLevelId(int levelId)
        {
            var lessons = await _lessonService.GetAllLessonsByLevelId(levelId);

            return Ok(lessons);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var lesson = await _lessonService.GetLessonById(id);
                
                return Ok(lesson);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateLessonDto dto)
        {
            try
            {
                var created = await _lessonService.CreateLesson(dto);
                
                return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateLessonDto dto)
        {
            try
            {
                await _lessonService.UpdateLesson(id, dto);
                
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _lessonService.DeleteLesson(id);

                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
