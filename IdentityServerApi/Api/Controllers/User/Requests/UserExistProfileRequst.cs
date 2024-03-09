namespace MicroServicesProject.Controllers.User.Requests;

public record UserExistProfileRequst
{
    public required Guid UserId { get; init; }
}