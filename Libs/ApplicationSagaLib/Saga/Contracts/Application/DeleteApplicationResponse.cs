namespace Services.Contracts.Application;

public class DeleteApplicationResponse: IMessage
{
    public Guid Id { get; set; }
}