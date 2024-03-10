using System.ComponentModel.DataAnnotations.Schema;
using ExampleCore.Dal.Base;


namespace IdentityServerDal.Roles.Models;

public record UserRole: BaseEntity<Guid>
{
    [ForeignKey("users")]
    public required Guid UserId { get; init; }
    
    [ForeignKey("role")]
    public required Guid RoleId { get; init; }
    
    public UserDal User { get; set; }
    public RoleDal Role { get; set; }
}