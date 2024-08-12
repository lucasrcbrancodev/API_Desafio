using API_Desafio.ApplicationServices.Dtos;

namespace API_Desafio.ApplicationServices.Users;

public interface IRandomUserGeneratorAPIService
{
    Task<List<RandomUserGeneratorApiResponseDto>> GetRandomUsersFromAPIAsync(int amount = 1, CancellationToken cancellationToken = default);
}