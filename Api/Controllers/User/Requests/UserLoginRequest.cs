using System.ComponentModel.DataAnnotations;

namespace MicroServicesProject.Controllers.User.Requests;

public record UserLoginRequest
{
    [MaxLength(256)]
    [EmailAddress]
    public required string Email { get; init; }
    
    [MinLength(6)]
    public required string Pasword { get; init; }
}