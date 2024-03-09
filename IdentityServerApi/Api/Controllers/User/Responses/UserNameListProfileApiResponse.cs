namespace MicroServicesProject.Controllers.User.Response;

public record UserNameListProfileApiResponse
{
    public required UserList UserList { get; set; }
}

public record UserList
{
    public required string Name { get; init; }
    
    public required Guid UserId { get; init; }
}