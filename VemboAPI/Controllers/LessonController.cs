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
        public IActionResult Get()
        {
            var lessons = _lessonService.GetAllLessons();
            return Ok(lessons);
        }

        [HttpGet("Level/{levelId}")]
        public IActionResult GetByLevelId(int levelId)
        {
            var lessons = _lessonService.GetAllLessonsByLevelId(levelId);

            return Ok(lessons);
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var lesson = _lessonService.GetLessonById(id);
                return Ok(lesson);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] CreateLessonDto dto)
        {
            try
            {
                var created = _lessonService.CreateLesson(dto);
                return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateLessonDto dto)
        {
            try
            {
                _lessonService.UpdateLesson(id, dto);
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
                _lessonService.DeleteLesson(id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
