using MoneyMap.API.Domain.DTOs;

namespace MoneyMap.API.Domain.UserDomain;

public interface IUserDomain
{
    Task<UserDto> CreateUserAsync(UserCreationDto user);
    Task<UserDto> GetUserByIdAsync(Guid id);
    Task<UserDto> AuthenticateAsync(AuthenticateUserDto loginDto);
}
