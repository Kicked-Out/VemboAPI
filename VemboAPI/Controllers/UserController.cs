using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
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
        if (dto == null || string.IsNullOrEmpty(dto.NickName) || string.IsNullOrEmpty(dto.Password) || string.IsNullOrEmpty(dto.Email))
            return BadRequest("Invalid user data.");

        _userService.CreateUser(dto);
        return Ok("User created successfully.");
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] UpdateUserDto dto)
    {
        if (dto == null || string.IsNullOrEmpty(dto.NickName) || string.IsNullOrEmpty(dto.Password) || string.IsNullOrEmpty(dto.Email))
            return BadRequest("Invalid user data.");

        try
        {
            _userService.UpdateUser(id, dto);
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
}
