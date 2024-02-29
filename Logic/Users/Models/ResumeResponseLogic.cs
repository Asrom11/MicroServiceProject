using IdentityServerDal.Roles.Models;

namespace IdentityServerLogic.Users.Models;

public class ResumeResponseLogic
{
    public required int Experience { get; init; }
    public required string Skills { get; init; }
    public required Education EducationLevel { get; init; }
    public required DateTime UpdateDateTime { get; init; }
}