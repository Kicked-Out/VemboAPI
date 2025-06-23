using Microsoft.AspNetCore.Mvc;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;

namespace VemboAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnitController : ControllerBase
    {
        private readonly IUnitService _unitService;

        public UnitController(IUnitService unitService)
        {
            _unitService = unitService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var units = _unitService.GetAllUnits();
            return Ok(units);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var unit = _unitService.GetUnitById(id);
                return Ok(unit);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Unit unit)
        {
            if (unit == null || string.IsNullOrEmpty(unit.Title))
            {
                return BadRequest("Invalid unit data.");
            }

            _unitService.CreateUnit(unit.Title, unit.Description, unit.Order, unit.TopicId);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Unit unit)
        {
            if (unit == null || string.IsNullOrEmpty(unit.Title))
            {
                return BadRequest("Invalid unit data.");
            }

            try
            {
                _unitService.UpdateUnit(id, unit.Title, unit.Description, unit.Order, unit.TopicId);
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
                _unitService.DeleteUnit(id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
