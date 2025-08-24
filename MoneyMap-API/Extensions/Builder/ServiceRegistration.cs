using Microsoft.EntityFrameworkCore;
using MoneyMap.API.Data.Context;

namespace MoneyMap.API.Extensions.Builder;

public static class ServiceRegistration
{
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("MoneyMapDb")));
    }
}