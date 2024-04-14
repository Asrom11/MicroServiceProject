using Domain.Entities;

namespace Api.Controllers.Applications;

public record UpdateApplicationRequest
{
    public required Guid VacancyId { get; init; }
    
    public required Guid ApplicationId { get; init; }
    
    public required Guid EmployerId { get; init; }

    public required ApplicationStatus Status { get; init; }
}