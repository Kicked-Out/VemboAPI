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

    private int? GetUserIdFromClaims()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                          ?? User.FindFirst("sub")?.Value;

        return int.TryParse(userIdClaim, out var userId) ? userId : (int?)null;
    }

    [Authorize]
    [HttpGet]
    public IActionResult GetAll()
    {
        var users = _userService.GetAllUsers();
        if (users == null || users.Count == 0)
            return NotFound("No users found.");
        return Ok(users);
    }

    [Authorize]
    [HttpGet("Current")]
    public IActionResult Get()
    {
        string userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value!;

        UserDto user = _userService.GetUserById(userId);

        return Ok(user);
    }

    [Authorize]
    [HttpGet("{id}")]
    public IActionResult Get(string id)
    {
        var user = _userService.GetUserById(id);
        if (user == null)
            return NotFound($"User with ID {id} not found.");
        return Ok(user);
    }

    [Authorize]
    [HttpGet("NickNameSlug/{nickNameSlug}")]
    public IActionResult GetByNickNameSlug(string nickNameSlug)
    {
        var user = _userService.GetUserByNickNameSlug(nickNameSlug);

        if (user == null)
        {
            return NotFound($"User with NickNameSlug {nickNameSlug} not found.");
        }

        return Ok(user);
    }

    [Authorize]
    [HttpPost]
    public IActionResult Post([FromBody] CreateUserDto dto)
    {
        if (dto == null ||
            string.IsNullOrWhiteSpace(dto.NickName) ||
            string.IsNullOrWhiteSpace(dto.Password) ||
            string.IsNullOrWhiteSpace(dto.Email))
            return BadRequest("Invalid user data.");

        _userService.CreateUser(dto);
        return Ok("User created successfully.");
    }

    [Authorize]
    [HttpPut("me")]
    public IActionResult UpdateSelf([FromBody] UpdateUserDto dto)
    {
        var userId = GetUserIdFromClaims();

        if (userId == null)
            return Unauthorized();

        try
        {
            _userService.UpdateUser(userId.Value, dto);
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
        var userId = GetUserIdFromClaims();

        if (userId == null)
            return Unauthorized();

        await _userService.UpdateRoleAsync(userId.Value, newRole);
        return Ok($"Role updated to {newRole}.");
    }

}
