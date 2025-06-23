using Microsoft.AspNetCore.Mvc;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;

namespace VemboAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeriodController : ControllerBase
    {
        private readonly IPeriodService _periodService;

        public PeriodController(IPeriodService periodService)
        {
            _periodService = periodService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var periods = _periodService.GetAllPeriods();
            return Ok(periods);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var period = _periodService.GetPeriodById(id);
                return Ok(period);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Period period)
        {
            if (period == null || string.IsNullOrEmpty(period.Title))
            {
                return BadRequest("Invalid period data.");
            }

            _periodService.CreatePeriod(period.Title, period.Description, period.ImageUrl);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Period period)
        {
            if (period == null || string.IsNullOrEmpty(period.Title))
            {
                return BadRequest("Invalid period data.");
            }

            try
            {
                _periodService.UpdatePeriod(id, period.Title, period.Description, period.ImageUrl);
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
                _periodService.DeletePeriod(id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
