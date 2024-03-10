namespace Api.Controllers.Vacancy.Requests;

public record CreateVacancyRequest
{
    public required Guid EmployerId { get; init; }
    
    public required  string Title { get; init; }
    
    public required string Description { get; init; }
    
}