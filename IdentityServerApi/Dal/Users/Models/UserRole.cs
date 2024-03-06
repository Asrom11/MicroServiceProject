using System.ComponentModel.DataAnnotations.Schema;


namespace IdentityServerDal.Roles.Models;

public class UserRole
{
    [ForeignKey("users")]
    public required Guid UserId { get; init; }
    
    [ForeignKey("role")]
    public required Guid RoleId { get; init; }
    
    public UserDal User { get; set; }
    public RoleDal Role { get; set; }
}