using Microsoft.AspNetCore.Mvc;
using MoneyMap.API.Domain.DTOs;
using MoneyMap.API.Domain.UserDomain;

namespace MoneyMap.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UserController(IUserDomain userDomain) : Controller
{
    [HttpGet]
    public async Task<ActionResult<UserDto>> GetUserByIdAsync(Guid id)
    {
        try
        {
            UserDto result = await userDomain.GetUserByIdAsync(id);

            return result;
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUserAsync(UserCreationDto user)
    {
        try
        {
            UserDto result = await userDomain.CreateUserAsync(user);

            return result;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> LoginAsync(AuthenticateUserDto loginDto)
    {
        try
        {
            UserDto user = await userDomain.AuthenticateAsync(loginDto);

            return user;
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
    }
}
