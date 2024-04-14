namespace Api.Controllers.Applications;

public record CreateApplicationsRequest
{
    public required Guid VacancyId { get; init; }
    public required Guid UserId { get; init; }
}