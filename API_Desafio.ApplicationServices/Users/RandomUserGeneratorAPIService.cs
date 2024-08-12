using API_Desafio.ApplicationServices.Dtos;
using API_Desafio.ApplicationServices.Users;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace API_Desafio.API.Services;

public class RandomUserGeneratorAPIService : IRandomUserGeneratorAPIService
{
    private const string BASE_URL = "https://randomuser.me/api";

    private readonly ILogger<RandomUserGeneratorAPIService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public RandomUserGeneratorAPIService(
        ILogger<RandomUserGeneratorAPIService> logger,
        IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<RandomUserGeneratorApiResponseDto>> GetRandomUsersFromAPIAsync(int amount = 1, CancellationToken cancellationToken = default)
    {
        List<RandomUserGeneratorApiResponseDto> users = [];

        for (int counter = 0; amount > counter; counter++)
        {
            var randomUser = await GetRandomUserFromAPIAsync(cancellationToken);
            if (randomUser is null)
            {
                continue;
            }

            users.Add(randomUser);
        }

        return users;
    }

    public async Task<RandomUserGeneratorApiResponseDto?> GetRandomUserFromAPIAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            HttpClient client = _httpClientFactory.CreateClient("RandomUserAPI");
            HttpRequestMessage message = new()
            {
                RequestUri = new Uri(BASE_URL),
                Method = HttpMethod.Get
            };

            HttpResponseMessage? apiResponse = await client.SendAsync(message, cancellationToken);
            var apiContent = await apiResponse.Content.ReadAsStringAsync(cancellationToken);

            return JsonConvert.DeserializeObject<RandomUserGeneratorApiResponseDto>(apiContent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }
}
