using Microsoft.AspNetCore.Identity;

namespace AuthModels.Entities;

public class ApplicationUser
{
    public int Id { get; set; }
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
