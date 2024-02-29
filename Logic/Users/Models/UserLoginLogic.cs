namespace IdentityServerLogic.Users.Models;

public record UserLoginLogic
{
    public required string Email { get; init; }

    public required string Pasword { get; init; }
}