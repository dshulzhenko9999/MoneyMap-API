using Microsoft.EntityFrameworkCore;
using MoneyMap.API.Data.Context;
using MoneyMap.API.Data.Models;

namespace MoneyMap.API.Data.Repositories.UserRepository.Implementations;

public class UserRepository(DatabaseContext context) : IUserRepository
{
    public async Task<User> SaveUserAsync(User user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();

        return user;
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}
