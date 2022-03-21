using LecAuth.Models;
using LecAuth.Models.Entities;
using LecAuth.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LecAuth.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUserRepository _userRepo;
    private readonly Random _random = new Random();

    public HomeController(IUserRepository userRepo, ILogger<HomeController> logger)
    {
        _logger = logger;
        _userRepo = userRepo;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult GetUserName()
    {
        if (User.Identity!.IsAuthenticated)
        {
            string username = User.Identity.Name ?? "";
            return Content(username);
        }
        return Content("No user");
    }

    public async Task<IActionResult> GetUserId()
    {
        if (User.Identity!.IsAuthenticated)
        {
            string username = User.Identity.Name ?? "";
            var user = await _userRepo.ReadAsync(username);
            if(user != null)
            {
                return Content(user.Id);
            }
        }
        return Content("No user");
    }


    public IActionResult Restricted()
    {
        return Content("This is restricted.");
    }

    [AllowAnonymous]
    public IActionResult About()
    {
        ViewData["Message"] = "Your application about page.";

        return View();
    }

    public async Task<IActionResult> CreateTestUser()
    {
        var n = _random.Next(100);
        var check = await _userRepo.ReadAsync($"test{n}@test.com");
        if (check == null)
        {
            var user = new ApplicationUser
            {
                Email = $"test{n}@test.com",
                UserName = $"test{n}@test.com",
                FirstName = $"User{n}",
                LastName = $"Userlastname{n}"
            };
            await _userRepo.CreateAsync(user, "Pass1!");
            return Content($"Created test user 'test{n}@test.com' with password 'Pass1!'");
        }
        return Content("The user was already created.");
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
}
