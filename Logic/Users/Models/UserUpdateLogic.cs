namespace IdentityServerLogic.Users.Models;

public class UserUpdateLogic
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
    public ResumeRequestLogic Resume { get; set; }
}