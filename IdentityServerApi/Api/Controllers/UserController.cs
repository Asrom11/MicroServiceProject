﻿using IdentityServerDal.Roles.Models;
using IdentityServerLogic.Users.Interfaces;
using IdentityServerLogic.Users.Models;
using MicroServicesProject.Controllers.User.Requests;
using MicroServicesProject.Controllers.User.Response;
using Microsoft.AspNetCore.Mvc;
using ProfileConnectionLib.ConnectionServices.DtoModels.UserNameLists;
using UserNameListProfileDto = MicroServicesProject.Controllers.User.Requests.UserNameListProfileDto;

namespace MicroServicesProject.Controllers;


[Route("api/user")]
[ApiController]
public class UserController: ControllerBase
{
    private readonly IUserLogicManager _userLogicManager;
    public UserController(IUserLogicManager userLogicManager)
    {
        _userLogicManager = userLogicManager;
    }
    
    [HttpPost("register")]
    [ProducesResponseType(typeof(CreateUserRepsonse), 200)]
    public async Task<IActionResult> RegisterAsync([FromBody] CreateUserRequests dto)
    {
          var res = await _userLogicManager.RegisterAsync(new UserLogic()
          {
              Name = dto.Name,
              Email = dto.Email,
              Pasword = dto.Pasword,
              Role = dto.Role
          });
        
        return res  ? Ok(new { Status = "Success" }) : StatusCode(500, "Failed, try again later");
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] UserLoginRequest dto)
    {
        var res = await _userLogicManager.LoginAsync(new UserLoginLogic()
        {
            Email = dto.Email,
            Pasword = dto.Pasword
        });
        return Ok(res);
    }

    [HttpGet]
    [ProducesResponseType<UserProfileResponse>(200)]
    public async Task<IActionResult> GetProfileAsync([FromQuery] Guid userId)
    {
        var res = _userLogicManager.GetProfileAsync(userId);
        return Ok(res);
    }
    
    
    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfileAsync([FromBody] UpdateUserRequest dto)
    {
        var res = await _userLogicManager.UpdateProfileAsync(new UserUpdateLogic
        {
            Id = dto.Id,
            Email = dto.Email,
            Name = dto.Name,
            Resume = new ResumeRequestLogic()
            {
                EducationLevel = dto.Resume.EducationLevel,
                Experience = dto.Resume.Experience,
                Skills = dto.Resume.Skills,
            }
        });
        
        return Ok(res);
    }
    
    
    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteProfileAsync([FromQuery] Guid userId)
    {
        var res = await _userLogicManager.DeleteProfileAsync(userId);
        return res is true ? Ok(new { Status = "Success", Message = "Profile deleted successfully." }) : StatusCode(500, "Failed, try again later");
    }

    [HttpPost("namelist")]
    [ProducesResponseType(typeof(UserNameInfo),200)]
    public async Task<IActionResult> GetUserNameListAsync([FromBody] UserNameListProfileDto userNameListProfileDto)
    {
        var res = await _userLogicManager.GetUserNameListAsync(userNameListProfileDto.UserIdList);
        
        var userNameInfos = res.Select(user => new UserNameListProfileApiResponse
        {
            UserList = new UserList
            {
                Name = user.Name,
                UserId = user.Id 
            }
        }).ToList();
        
        return Ok(userNameInfos);
    }

    
    [HttpPost("exist")]
    [ProducesResponseType(typeof(UserExistProfileDtoResponse),200)]
    public async Task<IActionResult> CheckUserExistProfile([FromBody] UserExistProfileDtoRequst userExistProfileDtoRequst)
    {
        var res = await _userLogicManager.CheckUserExist(userExistProfileDtoRequst.UserId);
        return Ok(new UserExistProfileDtoResponse()
        {
            UserId = res
        });
    }
}