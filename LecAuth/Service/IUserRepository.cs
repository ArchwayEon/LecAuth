using LecAuth.Models.Entities;

namespace LecAuth.Service;

public interface IUserRepository
{
    Task<ApplicationUser?> ReadAsync(string userName);
    Task<ApplicationUser> CreateAsync(ApplicationUser user, string password);
}

