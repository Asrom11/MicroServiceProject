using IdentityServerDal.Roles.Models;

namespace MicroServicesProject.Controllers.User.Requests;

public class UpdateUserRequest
{
    public required Guid Id;
    public required string Name;
    public required string Email;
    public ResumeRequest Resume { get; set; }
}