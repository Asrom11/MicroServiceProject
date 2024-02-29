using System.ComponentModel.DataAnnotations.Schema;
using Core.Dal.Base;


namespace IdentityServerDal.Roles.Models;

[Table("role")]
public class RoleDal: BaseEntityDal<Guid>
{
    public required string Name { get; init; }
    
    public  List<UserRole> UserRoles { get; set; }
}