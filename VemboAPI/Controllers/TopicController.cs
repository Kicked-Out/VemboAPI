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
        public IActionResult Post([FromBody] Topic topic)
        {
            if (topic == null || string.IsNullOrEmpty(topic.Title) || string.IsNullOrEmpty(topic.Description))
            {
                return BadRequest("Invalid topic data.");
            }

            var createdTopic = _topicService.CreateTopic(topic.Title, topic.Description);
            return CreatedAtAction(nameof(Get), new { id = createdTopic.Id }, createdTopic);
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
