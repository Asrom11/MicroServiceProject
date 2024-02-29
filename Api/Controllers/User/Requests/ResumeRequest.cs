using IdentityServerDal.Roles.Models;

namespace MicroServicesProject.Controllers.User.Requests;

public record ResumeRequest
{
    public required Guid UserID { get; set; }
    public required int Experience { get; init; }
    public required string Skills { get; init; }
    public required Education EducationLevel { get; init; }
}