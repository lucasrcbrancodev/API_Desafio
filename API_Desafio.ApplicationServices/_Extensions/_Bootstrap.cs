using API_Desafio.API.Services;
using API_Desafio.Application.Dtos;
using API_Desafio.Application.Services;
using API_Desafio.ApplicationServices.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API_Desafio.Application._Extensions;

public static class BootstrapApplicationServices
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHttpClient<IRandomUserGeneratorAPIService, RandomUserGeneratorAPIService>();

        services.AddScoped<IRandomUserGeneratorAPIService, RandomUserGeneratorAPIService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenGenerator, TokenGenerator>();
        services.AddScoped<ITokenProvider, TokenProvider>();

        services.Configure<JwtOptions>(configuration.GetSection("ApiSettings:JwtOptions"));

        return services;
    }
}
