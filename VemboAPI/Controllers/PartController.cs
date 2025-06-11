using Microsoft.AspNetCore.Mvc;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;

namespace VemboAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartController : ControllerBase
    {
        private IPartService _partService;

        public PartController(IPartService partService)
        {
            _partService = partService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var parts = _partService.GetAllParts();
            return Ok(parts);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var part = _partService.GetPartById(id);
                return Ok(part);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Part part)
        {
            if (part == null || string.IsNullOrEmpty(part.Title))
            {
                return BadRequest("Invalid part data.");
            }

            _partService.CreatePart(part.Title, part.TopicId);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Part part)
        {
            if (part == null || string.IsNullOrEmpty(part.Title))
            {
                return BadRequest("Invalid part data.");
            }

            try
            {
                _partService.UpdatePart(id, part.Title, part.TopicId);
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
                _partService.DeletePart(id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
