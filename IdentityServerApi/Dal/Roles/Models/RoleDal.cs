using System.ComponentModel.DataAnnotations.Schema;
using ExampleCore.Dal.Base;


namespace IdentityServerDal.Roles.Models;

[Table("role")]
public record RoleDal: BaseEntity<Guid>
{
    public required string Name { get; init; }
    
    public  List<UserRole> UserRoles { get; set; }
}