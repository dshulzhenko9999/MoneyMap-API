using Microsoft.AspNetCore.Mvc;
using MoneyMap.API.Domain.DTOs;
using MoneyMap.API.Domain.UserDomain;

namespace MoneyMap.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UserController(IUserDomain userDomain) : Controller
{
    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUserAsync(UserCreationDto user)
    {
        try
        {
            UserDto result = await userDomain.CreateUserAsync(user);

            return result;
        }
        catch (InvalidOperationException ex)
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
