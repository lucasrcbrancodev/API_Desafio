using API_Desafio.ApplicationServices.Dtos;

namespace API_Desafio.Application.Dtos;

public class LoginRequestDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class LoginResponseDto
{
    public string? Token { get; set; }
    public UserDto? User { get; set; }
}

