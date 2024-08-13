using API_Desafio.Application.Data;
using API_Desafio.Application.Dtos;
using API_Desafio.ApplicationServices.Dtos;
using API_Desafio.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace API_Desafio.Application.Services;

public class AuthService : IAuthService
{
    private readonly IApplicationDbContext _ctx;
    private readonly ITokenGenerator _tokenGenerator;

    public AuthService(IApplicationDbContext ctx, ITokenGenerator tokenGenerator)
    {
        _ctx = ctx;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
    {
        try
        {
            User? user = await _ctx.User.FirstAsync(u => u.ContactInformation.Email.ToUpper() == loginRequestDto.Email.ToUpper());
            bool isPasswordValid = IsPasswordValid(user, loginRequestDto);
            if (!isPasswordValid)
            {
                return new();
            }

            var token = _tokenGenerator.GenerateToken(user);

            return new()
            {
                User = new UserDto()
                {
                    Id = user.Id,
                    Email = user.ContactInformation.Email,
                    Username = user.LoginInformation.Username,
                    Name = user.NameInformation.First + " " + user.NameInformation.Last,
                    ImageInformation = new ImageInformation(user.ImageInformation.Thumbnail),
                    DateOfBirthInformation = new DateOfBirthInformation(
                        user.DateOfBirthInformation.DateOfBirth,
                        user.DateOfBirthInformation.Age)
                },
                Token = token
            };
        }
        catch
        {
            return new();
        }
    }

    private bool IsPasswordValid(User user, LoginRequestDto loginRequestDto)
    {
        var hashedPassword = GetHash(loginRequestDto.Password, user.LoginInformation.Salt);

        return Equals(user.LoginInformation.Password, hashedPassword);
    }

    private static string GetHash(string clearTextPassword, string salt)
    {
        //Convert the salted password to a byte array
        byte[] saltedHashBytes = Encoding.UTF8.GetBytes(clearTextPassword + salt);

        //Use hash algorithm to calulate hash
        HashAlgorithm algorithm = SHA256.Create();
        byte[] hash = algorithm.ComputeHash(saltedHashBytes);

        //Return the hash as a base64 encoded string to be compared and stored
        return Convert.ToBase64String(hash);
    }
}
