using MoneyMap.API.Data.Models;

namespace MoneyMap.API.Data.Repositories.UserRepository;

public interface IUserRepository
{
    Task<User> SaveUserAsync(User user);
    Task<User?> GetUserByEmailAsync(string email);
}
