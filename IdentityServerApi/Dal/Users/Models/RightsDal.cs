
using ExampleCore.Dal.Base;

namespace IdentityServerDal.Roles.Models;

public record RightsDal: BaseEntity<Guid>
{
    public string Name;
    
}