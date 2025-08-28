using MoneyMap.API.Data.Models;

namespace MoneyMap.API.Domain.DTOs;

public class UserCreationDto
{
    public Guid Id { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }

    public User CreateUser(byte[] passwordHash, byte[] passwordSalt)
    {
        return new User
        {
            Id = Id,
            UserName = UserName,
            Email = Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };
    }
}

public class UserDto
{
    public Guid Id { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public DateTime CreatedAt { get; set; }
     
    public static UserDto ToUserDto(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            CreatedAt = user.CreatedAt
        };
    }
}