namespace Api.Controllers.Feedback;

public class DeleteFeedbackRequest
{
    public required Guid FeedbackId { get; init; }
    
    public required Guid UserId { get; init; }
}