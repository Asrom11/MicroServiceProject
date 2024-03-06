
using ExampleCore.Dal.Base;

namespace IdentityServerDal.Roles.Models;

public record RightsDal: BaseEntityDal<Guid>
{
    public string Name;
    
}