using IdentityServerDal.Roles.Models;
using MicroServicesProject.Controllers.User.Response;

namespace IdentityServerDal.Roles.Interfaces;

public interface IUserRepository
{
    public Task<Guid> CreateAsync(UserDal userDal);
    public Task<UserDal> GetProfileAsyncByEmail(string email);
    public Task<UserDal> GetProfileAsyncById(Guid id);
    public Task<UpdatedResult> UpdateAsync(UserDal userDal);
    public Task<bool> DeleteAsync(Guid id);

    public Task<bool> CheckEmailAsync(string email);
    public Task<List<UserDal>> GetAllUser();

    public Task<Guid> CheckUserExist(Guid userId);
}