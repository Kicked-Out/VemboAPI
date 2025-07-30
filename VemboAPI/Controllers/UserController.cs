using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using VemboAPI.Domain.DTOs;
using VemboAPI.Infrastructure.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize]
    [HttpGet]
    public IActionResult Get()
    {
        var users = _userService.GetAllUsers();
        if (users == null || users.Count == 0)
            return NotFound("No users found.");
        return Ok(users);
    }

    [Authorize]
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var user = _userService.GetUserById(id);
        if (user == null)
            return NotFound($"User with ID {id} not found.");
        return Ok(user);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult Post([FromBody] CreateUserDto dto)
    {
        if (dto == null || string.IsNullOrEmpty(dto.NickName) || string.IsNullOrEmpty(dto.Password) || string.IsNullOrEmpty(dto.Email))
            return BadRequest("Invalid user data.");

        _userService.CreateUser(dto);
        return Ok("User created successfully.");
    }

    [Authorize]
    [HttpPut("me")]
    public IActionResult UpdateSelf([FromBody] UpdateUserDto dto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                          ?? User.FindFirst("sub")?.Value;

        if (userIdClaim == null)
            return Unauthorized();

        int userId = int.Parse(userIdClaim);

        try
        {
            _userService.UpdateUser(userId, dto);
            return Ok("User updated successfully.");
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }


    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            _userService.DeleteUser(id);
            return Ok("User deleted successfully.");
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    [Authorize]
    [HttpPut("me/role")]
    public async Task<IActionResult> UpdateMyRole([FromBody] string newRole)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                          ?? User.FindFirst("sub")?.Value;

        if (userIdClaim == null)
            return Unauthorized();

        int userId = int.Parse(userIdClaim);

        await _userService.UpdateRoleAsync(userId, newRole);
        return Ok($"Role updated to {newRole}.");
    }

}
