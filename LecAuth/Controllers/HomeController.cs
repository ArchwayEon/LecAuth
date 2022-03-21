using LecAuth.Models;
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

    public IActionResult GetUserId()
    {
        if (User.Identity!.IsAuthenticated)
        {
            string username = User.Identity.Name ?? "";
            var user = _userRepo.Read(username);
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
