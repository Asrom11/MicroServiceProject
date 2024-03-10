namespace MicroServicesProject.Controllers.User.Requests;

public record UserExistProfileDtoRequst
{
    public required Guid UserId { get; init; }
}