namespace Api.Controllers.Feedback;

public record UpdateFeedbackRequest: CreateFeedbackRequest
{
    public required Guid FeedBackId { get; init; }
}