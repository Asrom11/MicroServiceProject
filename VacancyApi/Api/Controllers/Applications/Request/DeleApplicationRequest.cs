namespace Api.Controllers.Applications;

public record DeleApplicationRequest
{
    public required Guid UserId { get; init; }
    public required Guid ApplicationId { get; init; }
}