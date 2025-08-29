using MoneyMap.API.Data.Models;
using MoneyMap.API.Domain.DTOs;

namespace MoneyMap.UnitTests.DomainTests.Mocks;

public class UserDomainTestsMocks
{
    public readonly static UserCreationDto UserCreationDto = new()
    {
        UserName = "Test User",
        Email = "test@mail.com",
        Password = "TestPassword123!"
    };

    public readonly static User User = new()
    {
        UserName = UserCreationDto.UserName,
        Email = UserCreationDto.Email,
        PasswordHash = [1, 2, 3],
        PasswordSalt = [4, 5, 6],
        CreatedAt = DateTime.UtcNow
    };

    public readonly static AuthenticateUserDto AuthenticateUserDto = new()
    {
        Email = "test@mail.com",
        Password = "TestPassword123!"
    };

    public readonly static byte[] expectedHash = [1, 2, 3];
    public readonly static byte[] expectedSalt = [4, 5, 6];
}
