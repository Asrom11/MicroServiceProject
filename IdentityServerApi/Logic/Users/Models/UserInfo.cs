namespace IdentityServerLogic.Users.Models;

public record UserNameInfo
{
    public required UserListLogic UserList { get; set; }
}


public record UserListLogic
{
    public required string Name { get; init; }
    
    public required Guid UserId { get; init; }
}