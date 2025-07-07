using Microsoft.AspNetCore.Mvc;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TopicController : ControllerBase
    {
        private readonly ITopicService _topicService;

        public TopicController(ITopicService topicService)
        {
            _topicService = topicService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var topics = _topicService.GetAllTopics();
            return Ok(topics);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var topic = _topicService.GetTopicById(id);
                return Ok(topic);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] TopicCreateDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Title) || string.IsNullOrEmpty(dto.Description))
            {
                return BadRequest("Invalid topic data.");
            }

            try
            {
                var createdTopic = _topicService.CreateTopic(
                    dto.Title,
                    dto.Description,
                    dto.ImageUrl,
                    dto.PeriodId
                );
                
                return CreatedAtAction(nameof(Get), new { id = createdTopic.Id }, createdTopic);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] TopicCreateDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Title) || string.IsNullOrEmpty(dto.Description))
            {
                return BadRequest("Invalid topic data.");
            }

            try
            {
                _topicService.UpdateTopic(
                    id,
                    dto.Title,
                    dto.Description,
                    dto.ImageUrl,
                    dto.PeriodId
                );

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
                _topicService.DeleteTopic(id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
