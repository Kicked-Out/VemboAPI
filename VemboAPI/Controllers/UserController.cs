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

    private string? GetUserIdFromClaims()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                          ?? User.FindFirst("sub")?.Value;

        return userIdClaim;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllUsers();

        if (users == null || users.Count == 0)
            return NotFound("No users found.");
        
        return Ok(users);
    }

    [Authorize]
    [HttpGet("Current")]
    public async Task<IActionResult> Get()
    {
        string userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value!;

        UserDto user = await _userService.GetUserById(userId);

        return Ok(user);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var user = await _userService.GetUserById(id);

        if (user == null)
            return NotFound($"User with ID {id} not found.");
        
        return Ok(user);
    }

    [Authorize]
    [HttpGet("NickNameSlug/{nickNameSlug}")]
    public async Task<IActionResult> GetByNickNameSlug(string nickNameSlug)
    {
        var user = await _userService.GetUserByNickNameSlug(nickNameSlug);

        if (user == null)
        {
            return NotFound($"User with NickNameSlug {nickNameSlug} not found.");
        }

        return Ok(user);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateUserDto dto)
    {
        if (dto == null ||
            string.IsNullOrWhiteSpace(dto.NickName) ||
            string.IsNullOrWhiteSpace(dto.Password) ||
            string.IsNullOrWhiteSpace(dto.Email))
            return BadRequest("Invalid user data.");

        await _userService.CreateUser(dto);

        return Ok("User created successfully.");
    }

    [Authorize]
    [HttpPut("me")]
    public async Task<IActionResult> UpdateSelf([FromForm] UpdateUserDto dto)
    {
        var userId = GetUserIdFromClaims();

        if (userId == null)
            return Unauthorized();

        try
        {
            await _userService.UpdateUser(userId, dto);
            
            return Ok("User updated successfully.");
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }


    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _userService.DeleteUser(id);
            
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

        await _userService.UpdateRoleAsync(userId, newRole);
        return Ok($"Role updated to {newRole}.");
    }

}
