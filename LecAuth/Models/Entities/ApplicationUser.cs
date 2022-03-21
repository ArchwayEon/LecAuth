using Microsoft.AspNetCore.Identity;

namespace LecAuth.Models.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
}

