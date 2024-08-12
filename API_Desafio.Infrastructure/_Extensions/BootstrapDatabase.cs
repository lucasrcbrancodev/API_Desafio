using API_Desafio.Application.Data;
using API_Desafio.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API_Desafio.Infrastructure._Extensions;

public static class BootstrapDatabase
{
    public static IServiceCollection ConfigureDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(opt =>
        {
            opt.UseNpgsql(configuration.GetConnectionString("Default")/*, b => b.MigrationsAssembly("API_Desafio.API")*/);
        });

        return services;
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        return services;
    }

    public static IServiceProvider ExecutePendingMigrations(
        this IServiceProvider serviceProvider,
        bool isDevelopment,
        IEnumerable<string> pendingMigrations)
    {
        using var serviceScope = serviceProvider.GetService<IServiceScopeFactory>()?.CreateScope();

        if (serviceScope is null)
        {
            throw new Exception("Could not preload database.");
        }

        var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (isDevelopment)
        {
            if (pendingMigrations.Any())
            {
                context.Database.Migrate();
            }
        }

        return serviceProvider;
    }
}
