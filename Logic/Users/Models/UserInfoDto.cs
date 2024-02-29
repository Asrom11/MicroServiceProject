using IdentityServerDal.Roles.Models;

namespace IdentityServerLogic.Users.Models;

public class UserInfo
{
    public required string Name { get; init; }
    
    public required string Email { get; init; }

    public required ResumeResponseLogic Resume { get; init; }
}