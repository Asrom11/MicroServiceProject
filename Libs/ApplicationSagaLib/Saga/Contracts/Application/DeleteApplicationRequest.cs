namespace Services.Contracts.Application;

public class DeleteApplicationRequest: IMessage
{
    public required Guid Id { get; set; }
    
    public required Guid UserId { get; set; }
}