using IdentityServerDal.Roles.Models;

namespace IdentityServerLogic.Roles.Interfaces;

public interface IRoleLogicManager
{
    public Task<bool> CreateRoleAsync(RoleDal role);

    public Task<string> RoleExistsAsync(string name);

    public Task<bool> UpdateRoleAsync(RoleDal role);

    public Task<bool> DeleteRoleAsync(Guid roleId);

    public Task AddUserToRoleAsync(Guid userId, Users.Models.Roles roleName);

    public Task RemoveRoleFromUserAsync(Guid userId, Users.Models.Roles roleId);
}