using IdentityServerDal.Roles.Models;

namespace IdentityServerDal.Roles.Interfaces;

public interface IRoleRepository
{
    public Task<bool> CreateRoleAsync(RoleDal role);

    public Task<RoleDal> GetRoleByNameAsync(string roleName);

    public Task<bool> UpdateRoleAsync(RoleDal role);

    public Task<bool> DeleteRoleAsync(Guid roleId);

    public Task AddUserToRoleAsync(Guid userId, RoleDal roleDal);

    public Task RemoveRoleFromUserAsync(Guid userId, RoleDal roleDal);

    public Task<string> CheckRoleByNameAsync(string name);
}