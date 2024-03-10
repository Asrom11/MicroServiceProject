namespace MicroServicesProject.Controllers.User.Requests;

public record UserNameListProfileDto
{
    public required Guid[] UserIdList { get; init; }
}