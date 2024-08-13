using API_Desafio.Application.Dtos;
using API_Desafio.Application.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace API_Desafio.API.Controllers;
public class AuthController : Controller
{
    public const string HOME_CONTROLLER = "Home";
    private readonly IAuthService _authenticationService;
    private readonly ITokenProvider _tokenProvider;

    public AuthController(IAuthService authenticationService, ITokenProvider tokenProvider)
    {
        _authenticationService = authenticationService;
        _tokenProvider = tokenProvider;
    }

    [HttpGet]
    public IActionResult Login()
    {
        LoginRequestDto loginRequestDto = new();

        if (string.IsNullOrEmpty(_tokenProvider.GetToken()))
        {
            return View(loginRequestDto);
        }
        else
        {
            TempData["error"] = $"You are already logged in.";
            return RedirectToAction(nameof(Index), HOME_CONTROLLER);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
    {
        var loginResult = await _authenticationService.LoginAsync(loginRequestDto);

        if (!string.IsNullOrEmpty(loginResult.Token))
        {
            _tokenProvider.SetToken(loginResult.Token!);
            await SignInUserAsync(loginResult);
            return RedirectToAction(nameof(Index), HOME_CONTROLLER);
        }
        else
        {
            TempData["error"] = $"Email or Password not valid.";
            return View(loginRequestDto);
        }
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        _tokenProvider.ClearToken();
        return RedirectToAction(nameof(Index), HOME_CONTROLLER);
    }

    private async Task SignInUserAsync(LoginResponseDto loginResponseDto)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(loginResponseDto.Token);
        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, token.Claims.FirstOrDefault(t => t.Type == JwtRegisteredClaimNames.Sub).Value));
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, token.Claims.FirstOrDefault(t => t.Type == JwtRegisteredClaimNames.Email).Value));
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name, token.Claims.FirstOrDefault(t => t.Type == JwtRegisteredClaimNames.Name).Value));


        var principal = new ClaimsPrincipal(identity);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
    }
}