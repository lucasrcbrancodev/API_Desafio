using API_Desafio.API.Models;
using API_Desafio.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Desafio.API.Controllers;

public class UserController : Controller
{
    public const string REDIRECT_TO_HOME = "Home";

    private readonly ApplicationDbContext _ctx;
    private readonly ILogger<UserController> _logger;

    public UserController(ApplicationDbContext ctx, ILogger<UserController> logger)
    {
        _ctx = ctx;
        _logger = logger;
    }

    [Authorize]
    public async Task<IActionResult> EditUser(Guid userId)
    {
        if (ModelState.IsValid)
        {
            var user = await _ctx.User.FirstOrDefaultAsync(u => u.Id == userId);
            if (user is not null)
            {
                return View(EditUserRequest.CreateFromUser(user));
            }
        }

        TempData["error"] = $"Could not find the user with id {userId}";
        return NotFound();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> EditUser(EditUserRequest model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var user = await _ctx.User.FirstOrDefaultAsync(u => u.Id == model.Id);
                if (user is null)
                {
                    TempData["error"] = $"Could not find the user with id {model.Id}";
                    return RedirectToAction(nameof(Index));
                }

                user.Update(
                    model.Username,
                    model.DocumentNumber,
                    model.DateOfBirth,
                    model.First,
                    model.Last,
                    model.Thumbnail);

                await _ctx.SaveChangesAsync();
                TempData["success"] = "User edited successfully";
                return RedirectToAction(nameof(Index), REDIRECT_TO_HOME);
            }

            TempData["error"] = "Error updating user";
            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError("{0} - {1}", ex.Message, ex.InnerException);
            TempData["error"] = "Error updating user";
            return View(model);

        }
    }
}
