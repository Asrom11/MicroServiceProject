using IdentityServerDal.Roles.Interfaces;
using IdentityServerDal.Roles.Models;
using Medo;
using Microsoft.EntityFrameworkCore;

namespace IdentityServerDal.Roles;

public class RoleRepository: IRoleRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    public RoleRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }


    public async Task<bool> CreateRoleAsync(RoleDal role)
    {
        await using var transaction = _applicationDbContext.Database.BeginTransaction();
        try
        {
            var roleId = new Uuid7().ToGuid();
            role.Id = roleId;
            await _applicationDbContext.Roles.AddAsync(role);
            await _applicationDbContext.SaveChangesAsync();
            transaction.Commit();
            
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            transaction.Rollback();
            return false;
        }
    }

    public  async Task<RoleDal> GetRoleByNameAsync(string roleName)
    {
        return (await _applicationDbContext.Roles
            .FirstOrDefaultAsync(r => r.Name == roleName))!;
    }

    public async Task<bool> UpdateRoleAsync(RoleDal role)
    {
        await using var transaction = _applicationDbContext.Database.BeginTransaction();
        try
        {
            _applicationDbContext.Roles.Update(role);
            await _applicationDbContext.SaveChangesAsync();
            transaction.Commit();

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            transaction.Rollback();
            return false;
        }
    }

    public async Task<bool> DeleteRoleAsync(Guid roleId)
    {
        await using var transaction = _applicationDbContext.Database.BeginTransaction();
        try
        {
            var role = await _applicationDbContext.Roles.FindAsync(roleId);
            if (role == null)
            {
                return false;
            }

            _applicationDbContext.Roles.Remove(role);
            await _applicationDbContext.SaveChangesAsync();
            transaction.Commit();
                
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            transaction.Rollback();
            return false;
        }
    }

    public async Task AddUserToRoleAsync(Guid userId, RoleDal roleDal)
    {
        await using var transaction = _applicationDbContext.Database.BeginTransaction();
        try
        {
            var userRole = new UserRole { UserId = userId, RoleId = roleDal.Id };
            await _applicationDbContext.UserRoles.AddAsync(userRole);
            await _applicationDbContext.SaveChangesAsync();

            transaction.Commit();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            transaction.Rollback();
            throw;
        }
    }

    public async Task RemoveRoleFromUserAsync(Guid userId, RoleDal roleDal)
    {
        await using var transaction = _applicationDbContext.Database.BeginTransaction();
        try
        {
            var userRole = await _applicationDbContext.UserRoles
                .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleDal.Id);

            if (userRole == null)
            {
                throw new Exception("Связь между пользователем и ролью не найдена");
            }

            _applicationDbContext.UserRoles.Remove(userRole);
            await _applicationDbContext.SaveChangesAsync();

            transaction.Commit();
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task<bool> CheckRoleByNameAsync(string name)
    {
        return await _applicationDbContext.Roles
            .AnyAsync(r => r.Name == name);
    }
}