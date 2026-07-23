using DemoMVC.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public static class Database
{
    public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection")));
        return services;
    }
}