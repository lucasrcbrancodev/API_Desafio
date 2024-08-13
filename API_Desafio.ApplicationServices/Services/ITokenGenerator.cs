using API_Desafio.Domain.Models;

namespace API_Desafio.Application.Services
{
    public interface ITokenGenerator
    {
        string GenerateToken(User user);
    }
}