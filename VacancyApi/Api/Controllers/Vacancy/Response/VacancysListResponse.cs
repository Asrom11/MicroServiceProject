namespace Api.Controllers.Vacancy.Response;

public record VacancysListResponse
{
    
    public required VacancyResponse[] VacancyResponses { get; init; }
    
}


public record VacancyResponse
{
    public required Guid VacancyId { get; init; }
    
    public required string Title { get; init; }
    
    public required string Description { get; init;}
    
    public required string Name { get; init; }
}