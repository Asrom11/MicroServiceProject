using IdentityServerDal.Roles.Models;

namespace IdentityServerLogic.Users.Models;

public record ResumeRequestLogic
{
    public required int Experience { get; init; }
    public required string Skills { get; init; }
    public required Education EducationLevel { get; init; }
}