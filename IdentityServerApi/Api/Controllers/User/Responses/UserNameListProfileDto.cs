namespace MicroServicesProject.Controllers.User.Response;

public record UserNameListProfileDto
{
    public required UserListProfileDto UserListProfileDto { get; set; }
}

public record UserListProfileDto
{
    public required string Name { get; init; }
    
    public required Guid UserId { get; init; }
}