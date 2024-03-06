using System.ComponentModel.DataAnnotations;
using IdentityServerLogic.Users.Models;

namespace MicroServicesProject.Controllers.User.Requests;

public record CreateUserRequests
{
    [MaxLength(256)]
    public required string Name { get; init; }
    
    [MaxLength(255)]
    [EmailAddress]
    public required string Email { get; init; }
    
    [MinLength(6)]
    public required string Pasword { get; init; }
    
    public required  Roles Role { get; init; }
}