using API_Desafio.API.Services;
using API_Desafio.ApplicationServices.Users;
using Microsoft.Extensions.DependencyInjection;

namespace API_Desafio.Application._Extensions;

public static class BootstrapApplicationServices
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddHttpClient<IRandomUserGeneratorAPIService, RandomUserGeneratorAPIService>();
        services.AddScoped<IRandomUserGeneratorAPIService, RandomUserGeneratorAPIService>();

        return services;
    }
}
