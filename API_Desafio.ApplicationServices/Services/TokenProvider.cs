using Microsoft.AspNetCore.Http;

namespace API_Desafio.Application.Services;

public class TokenProvider : ITokenProvider
{
    private const string TOKEN_TYPE = "JWTToken";
    private readonly IHttpContextAccessor _contextAccessor;

    public TokenProvider(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public string? GetToken()
    {
        string? token = null;
        var hasToken = _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(TOKEN_TYPE, out token);

        return hasToken is true ? token : null;
    }

    public void SetToken(string token)
    {
        _contextAccessor.HttpContext?.Response.Cookies.Append(TOKEN_TYPE, token);
    }

    public void ClearToken()
    {
        _contextAccessor.HttpContext?.Response.Cookies.Delete(TOKEN_TYPE);
    }
}