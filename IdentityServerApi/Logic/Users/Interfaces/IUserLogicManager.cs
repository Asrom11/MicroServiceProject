using IdentityServerDal.Roles.Models;
using IdentityServerLogic.Users.Models;
using MicroServicesProject.Controllers.User.Response;

namespace IdentityServerLogic.Users.Interfaces;

public interface IUserLogicManager
{
    Task<bool> RegisterAsync(UserLogic userLogic);
    Task<Guid> LoginAsync(UserLoginLogic userLoginLogic);
    Task<UserInfo> GetProfileAsync(Guid id);
    Task<UpdatedResult> UpdateProfileAsync(UserUpdateLogic userLogic);
    Task<bool> DeleteProfileAsync(Guid id);

    Task<Guid> CheckUserExist(Guid userId);

    Task<List<UserNameInfo>> GetUserNameListAsync();
}