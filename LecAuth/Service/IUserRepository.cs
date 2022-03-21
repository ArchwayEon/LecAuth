using LecAuth.Models.Entities;

namespace LecAuth.Service;

public interface IUserRepository
{
    ApplicationUser? Read(string userName);
}

