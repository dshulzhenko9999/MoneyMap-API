namespace MoneyMap.API.Domain.DTOs;

public class AuthenticateUserDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
