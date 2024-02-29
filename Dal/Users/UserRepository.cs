using IdentityServerDal.Roles.Interfaces;
using IdentityServerDal.Roles.Models;
using Medo;
using MicroServicesProject.Controllers.User.Response;
using Microsoft.EntityFrameworkCore;

namespace IdentityServerDal.Roles;

public class UserRepository: IUserRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    
    public UserRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    public async Task<Guid> CreateAsync(UserDal userDal)
    {
        await using var transaction = _applicationDbContext.Database.BeginTransaction();
        try
        {
            var userId = new Uuid7().ToGuid();
            userDal.Id = userId;
            userDal.Resume.Id = new Uuid7().ToGuid();
            var dbSet = _applicationDbContext.Users;
            await dbSet.AddAsync(userDal);
            await _applicationDbContext.SaveChangesAsync();

            transaction.Commit();
            return userId; 
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            transaction.Rollback();
            throw;
        }
    }
    public async Task<UserDal> GetProfileAsyncByEmail(string email)
    {
        return (await _applicationDbContext.Users
            .Include(u => u.Resume)
            .Include(u => u.UserRole)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Email == email))!;
    }

    public async Task<UserDal> GetProfileAsyncById(Guid id)
    {
        return (await _applicationDbContext.Users
            .Include(u => u.Resume)
            .Include(u => u.UserRole)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Id == id))!;
    }

    public async Task<UpdatedResult> UpdateAsync(UserDal userDal)
    { 
        await using var transaction = _applicationDbContext.Database.BeginTransaction();
        try
        {
            _applicationDbContext.Users.Update(userDal);
            await _applicationDbContext.SaveChangesAsync();
            transaction.Commit();

            return new UpdatedResult() { Success = true, Message = "Профиль успешно обновлен" };;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            transaction.Rollback();
            return new UpdatedResult { Success = true, Message = "Профиль успешно обновлен" };; 
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        await using var transaction = _applicationDbContext.Database.BeginTransaction();
        try
        {
            var user = await _applicationDbContext.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            _applicationDbContext.Users.Remove(user);
            return await _applicationDbContext.SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            transaction.Rollback();
            return false; 
        }
    }

    public async Task<bool> CheckEmailAsync(string email)
    {
        return await _applicationDbContext.Users
            .AnyAsync(u => u.Email == email);
    }
}