using MoneyMap.API.Data.Models;
using MoneyMap.API.Data.Repositories.UserRepository;
using MoneyMap.API.Domain.DTOs;
using MoneyMap.API.Domain.Services.PasswordService;

namespace MoneyMap.API.Domain.UserDomain.Implementations;

public class UserDomain(IPasswordService passwordService, IUserRepository userRepository) : IUserDomain
{
    public async Task<UserDto> CreateUserAsync(UserCreationDto user)
    {
        if (await userRepository.GetUserByIdAsync(user.Id) != null)
        {
            throw new InvalidOperationException("User with the same ID already exists");
        }

        passwordService.CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
        User newUser = user.CreateUser(passwordHash, passwordSalt);

        var createdUser = await userRepository.SaveUserAsync(newUser);

        return UserDto.ToUserDto(createdUser);
    }

    public async Task<UserDto> GetUserByIdAsync(Guid id)
    {
        var user = await userRepository.GetUserByIdAsync(id);

        return user == null ? throw new InvalidOperationException("User not found") : UserDto.ToUserDto(user);
    }

    public async Task<UserDto> AuthenticateAsync(AuthenticateUserDto loginRequest)
    {
        var user = await userRepository.GetUserByEmailAsync(loginRequest.Email);
        if (user == null || !passwordService.VerifyPasswordHash(loginRequest.Password, user.PasswordHash, user.PasswordSalt))
        {
            throw new UnauthorizedAccessException("Invalid email or password");
        }

        return UserDto.ToUserDto(user);
    }
}
