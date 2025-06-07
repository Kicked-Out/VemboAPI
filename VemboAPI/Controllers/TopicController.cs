using Microsoft.AspNetCore.Mvc;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;

namespace VemboAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TopicController : Controller
    {
        private ITopicService _topicService;

        public TopicController(ITopicService topicService)
        {
            _topicService = topicService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var topics = _topicService.GetAllTopics();
            if (topics == null || topics.Count == 0)
            {
                return NotFound("No topics found.");
            }
            return Ok(topics);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var topic = _topicService.GetTopicById(id);
            if (topic == null)
            {
                return NotFound($"Topic with ID {id} not found.");
            }
            return Ok(topic);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Topic topic)
        {
            if (topic == null || string.IsNullOrEmpty(topic.Title) || string.IsNullOrEmpty(topic.Description))
            {
                return BadRequest("Invalid topic data.");
            }

            _topicService.CreateTopic(topic.Title, topic.Description);
            return CreatedAtAction(nameof(Get), new { id = topic.Id }, topic);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Topic topic)
        {
            if (topic == null || string.IsNullOrEmpty(topic.Title) || string.IsNullOrEmpty(topic.Description))
            {
                return BadRequest("Invalid topic data.");
            }

            try
            {
                _topicService.UpdateTopic(id, topic.Title, topic.Description);
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
