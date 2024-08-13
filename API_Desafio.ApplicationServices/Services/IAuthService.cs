using API_Desafio.Application.Dtos;

namespace API_Desafio.Application.Services;

public interface IAuthService
{
    Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
}