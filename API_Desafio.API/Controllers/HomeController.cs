using API_Desafio.API.Models;
using API_Desafio.ApplicationServices.Dtos;
using API_Desafio.ApplicationServices.Users;
using API_Desafio.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace API_Desafio.API.Controllers;

public class HomeController : Controller
{
    private readonly IRandomUserGeneratorAPIService _randomUserGeneratorService;
    private readonly ApplicationDbContext _ctx;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, IRandomUserGeneratorAPIService randomUserGeneratorService, ApplicationDbContext ctx)
    {
        _logger = logger;
        _randomUserGeneratorService = randomUserGeneratorService;
        _ctx = ctx;
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        var users = await _ctx.User.ToListAsync();

        List<UserDto> usersReturnList = new();
        foreach (var user in users)
        {
            usersReturnList.Add(UserDto.CreateFromUser(user));
        }

        return View(usersReturnList.OrderBy(u => u.Name).ThenBy(u => u.Email));
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult Edit()
    {
        return View();
    }
}
