using MoneyMap.API.Data.Repositories.UserRepository;
using MoneyMap.API.Data.Repositories.UserRepository.Implementations;
using MoneyMap.API.Domain.Services.PasswordService;
using MoneyMap.API.Domain.Services.PasswordService.Implementations;
using MoneyMap.API.Domain.UserDomain;
using MoneyMap.API.Domain.UserDomain.Implementations;

namespace MoneyMap.API.Extensions.Builder;

public static class ServiceRegistration
{
    private static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IPasswordService, UserPasswordService>();
        builder.Services.AddScoped<IUserDomain, UserDomain>();
    }

    private static void RegisterRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUserRepository, UserRepository>();
    }

    public static void RegisterAllDependancies(this WebApplicationBuilder builder)
    {
        builder.RegisterServices();
        builder.RegisterRepositories();
    }
}