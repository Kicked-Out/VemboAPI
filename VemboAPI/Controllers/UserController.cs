using Microsoft.AspNetCore.Mvc;
using VemboAPI.Application.Interfaces;
using VemboAPI.Domain.Entities;

namespace VemboAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var users = _userService.GetAllUsers();
            if (users == null || users.Count == 0)
            {
                return NotFound("No users found.");
            }
            return Ok(users);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            if (user == null || string.IsNullOrEmpty(user.NickName) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Email))
            {
                return BadRequest("Invalid user data.");
            }
            _userService.CreateUser(user.NickName, user.Password, user.Email);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] User user)
        {
            if (user == null || string.IsNullOrEmpty(user.NickName) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Email))
            {
                return BadRequest("Invalid user data.");
            }
            try
            {
                _userService.UpdateUser(id, user.NickName, user.Password, user.Email);
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
                _userService.DeleteUser(id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
