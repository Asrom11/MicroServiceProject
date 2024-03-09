using IdentityServerDal.Roles.Interfaces;
using IdentityServerDal.Roles.Models;
using IdentityServerLogic.Roles.Interfaces;

namespace IdentityServerLogic.Roles;

public class RoleLogicManager: IRoleLogicManager
{
    private readonly IRoleRepository _roleRepository;
    public RoleLogicManager(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }
    public async Task<bool> CreateRoleAsync(RoleDal role)
    {
        if (await _roleRepository.CheckRoleByNameAsync(role.Name) is not null)
        {
            return false;
        }
        
        var status = await _roleRepository.CreateRoleAsync(role);
        return status;
    }

    public async Task<string> RoleExistsAsync(string name)
    {
        var status = await _roleRepository.CheckRoleByNameAsync(name);
        return status;
    }

    public async Task<bool> UpdateRoleAsync(RoleDal role)
    {
        var status = await _roleRepository.UpdateRoleAsync(role);
        return status;
    }

    public async Task<bool> DeleteRoleAsync(Guid roleId)
    {
        var status = await _roleRepository.DeleteRoleAsync(roleId);
        return status;
    }

    public async Task AddUserToRoleAsync(Guid userId, Users.Models.Roles roleName)
    {
        var role = await _roleRepository.GetRoleByNameAsync(roleName.ToString());
        await _roleRepository.AddUserToRoleAsync(userId, role);
    }


    public async Task RemoveRoleFromUserAsync(Guid userId, Users.Models.Roles roleName)
    {
        var role = await _roleRepository.GetRoleByNameAsync(roleName.ToString());
        await _roleRepository.RemoveRoleFromUserAsync(userId, role);
    }
}