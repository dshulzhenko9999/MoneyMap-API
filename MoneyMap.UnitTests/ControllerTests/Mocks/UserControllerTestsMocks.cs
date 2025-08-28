using MoneyMap.API.Domain.DTOs;

namespace MoneyMap.UnitTests.ControllerTests.Mocks;

public static class UserControllerTestsMocks
{
    public readonly static UserDto UserDto = new()
    {
        Id = Guid.NewGuid(),
        UserName = "Test User",
        Email = "test@mail.com"
    };

    public readonly static UserCreationDto UserCreationDto = new()
    {
        UserName = "Test User",
        Email = "test@mail.com",
        Password = "TestPassword123!"
    };

    public readonly static AuthenticateUserDto AuthenticateUserDto = new()
    {
        Email = "test@mail.com",
        Password = "TestPassword123!"
    };
}
