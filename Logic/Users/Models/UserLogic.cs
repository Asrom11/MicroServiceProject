namespace IdentityServerLogic.Users.Models;

public class UserLogic
{
    public required string Name { get; init; }
    
    public required string Email { get; init; }
    
    public required string Pasword { get; init; }
    
    public required  Roles Role { get; init; }
    
}

public enum Roles
{
    Employer,
    User,
}