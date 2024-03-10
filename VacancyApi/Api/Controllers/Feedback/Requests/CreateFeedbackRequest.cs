namespace Api.Controllers.Feedback;

public record CreateFeedbackRequest
{
    public required Guid VacancyId { get; init; }
    
    public required Guid UserId { get; init; }
    
    public required string Comment { get; init; }
    
    public required double Rating { get; set; }
}