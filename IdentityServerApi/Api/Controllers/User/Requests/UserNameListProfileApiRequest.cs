namespace MicroServicesProject.Controllers.User.Requests;

public record UserNameListProfileApiRequest
{
    public required Guid[] UserIdList { get; init; }
}