using LecAuth.Models.Entities;
using LecAuth.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LecAuth.Service;

public class DbUserRepository : IUserRepository
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public DbUserRepository(ApplicationDbContext db,
       UserManager<ApplicationUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }


    public async Task<ApplicationUser?> ReadAsync(string userName)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        return user;
    }

    public async Task<ApplicationUser> CreateAsync(
        ApplicationUser user, string password)
    {
        await _userManager.CreateAsync(user, password);
        return user;
    }

}

