using System.Security.Policy;
using BCrypt.Net;
using IdentityServerDal.Roles.Interfaces;
using IdentityServerDal.Roles.Models;
using IdentityServerLogic.Roles.Interfaces;
using IdentityServerLogic.Users.Interfaces;
using IdentityServerLogic.Users.Models;
using Medo;
using MicroServicesProject.Controllers.User.Response;

namespace IdentityServerLogic.Users;

public class UserLogicManager: IUserLogicManager
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleLogicManager _roleLogicManager;
    public UserLogicManager(IUserRepository userRepository, IRoleLogicManager logicManager)
    {
        _userRepository = userRepository;
        _roleLogicManager = logicManager;
    }

    public async Task<bool> RegisterAsync(UserLogic userLogic)
    {
        if (await _userRepository.CheckEmailAsync(userLogic.Email))
        {
            throw new Exception("Current Email Exist");
        }

        var paswordHash = BCrypt.Net.BCrypt.HashPassword(userLogic.Pasword);
        var user = new UserDal()
        {
            Email = userLogic.Email,
            Name = userLogic.Name,
            PaswordHash = paswordHash,
            Resume = new Resume()
            {
                EducationLevel = Education.None,
                Experience = 0,
                Id = new Uuid7().ToGuid(),
                UpdateDateTime = DateTime.Now.ToUniversalTime(),
                Skills = "None"
            },
            UserRole = [],
        };

        var createdResult = await _userRepository.CreateAsync(user);

        if (createdResult == Guid.Empty)
        {
            return false;
        }
        
        if (await _roleLogicManager.RoleExistsAsync(userLogic.Role.ToString()) is null)
        {
            
            var roleDal = new RoleDal()
            {
                Name = userLogic.Role.ToString(),
                UserRoles = []
            };
            await _roleLogicManager.CreateRoleAsync(roleDal);
        }

        await _roleLogicManager.AddUserToRoleAsync(createdResult, userLogic.Role);

        return true;
    }

    public async Task<Guid> LoginAsync(UserLoginLogic userLoginLogic)
    {
        var user = await _userRepository.GetProfileAsyncByEmail(userLoginLogic.Email);

        if (user is null || !BCrypt.Net.BCrypt.Verify(userLoginLogic.Pasword, user.PaswordHash))
        {
            throw new Exception("Wrong Login or Pasword");
        }
        
        // Дальше тут будет работа с сессиями... и в будщем тут вернётся не Guid, а SessionToken and RefreshToken

        return user.Id;
    }

    public async Task<UserInfo> GetProfileAsync(Guid id)
    {
        var user = await _userRepository.GetProfileAsyncById(id);

        if (user is null)
        {
            throw new Exception("User Not Found");
        }


        var resume = user.Resume;
        return new UserInfo()
        {
            Email = user.Email,
            Name = user.Name,
            Resume = new ResumeResponseLogic()
            {
                Experience = resume.Experience,
                EducationLevel = resume.EducationLevel,
                UpdateDateTime = resume.UpdateDateTime,
                Skills = resume.Skills
            }
        };
    }

    public async Task<UpdatedResult> UpdateProfileAsync(UserUpdateLogic userLogic)
    {

        var user = await _userRepository.GetProfileAsyncById(userLogic.Id);
        var newResume = userLogic.Resume;
        user.Name = userLogic.Name;
        user.Resume.EducationLevel = newResume.EducationLevel;
        user.Resume.Experience = newResume.Experience;
        user.Resume.Skills = newResume.Skills;
        user.Resume.UpdateDateTime = DateTime.Now.ToUniversalTime();
        
        var updatedResult = await _userRepository.UpdateAsync(user);
        return updatedResult;
    }

    public async Task<bool> DeleteProfileAsync(Guid id)
    {
        var status = await _userRepository.DeleteAsync(id);
        return status;
    }

    public async Task<Guid> CheckUserExist(Guid userId)
    {
        var user = await _userRepository.GetProfileAsyncById(userId);
        return user.Id;
    }

    public async Task<List<UserNameInfo>> GetUserNameListAsync()
    {
        var users = await _userRepository.GetAllUser();
        var userNameInfos = users.Select(user => new UserNameInfo
        {
            UserList = new UserListLogic
            {
                Name = user.Name,
                UserId = user.Id 
            }
        }).ToList();

        return userNameInfos;
    }
}