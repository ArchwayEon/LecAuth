using LecAuth.Models.Entities;
using LecAuth.Services;

namespace LecAuth.Service;

public class DbUserRepository : IUserRepository
{
    private readonly ApplicationDbContext _db;

    public DbUserRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public ApplicationUser? Read(string userName)
    {
        return _db.Users.FirstOrDefault(u => u.UserName == userName);
    }
}

