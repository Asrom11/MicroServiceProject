﻿using IdentityServerDal.Roles.Models;
using IdentityServerLogic.Users.Interfaces;
using IdentityServerLogic.Users.Models;
using MicroServicesProject.Controllers.User.Requests;
using MicroServicesProject.Controllers.User.Response;
using Microsoft.AspNetCore.Mvc;

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
        
        return Ok(new { Status = "Success" });
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
    
    
    [HttpDelete("profile")]
    public async Task<IActionResult> DeleteProfileAsync([FromQuery] Guid userId)
    {
        var res = await _userLogicManager.DeleteProfileAsync(userId);
        return Ok(new { Status = "Success", Message = "Profile deleted successfully." });
    }
}