using Api.Controllers.Applications;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Api.Controllers;

[ApiController]
[Route("api/application")]
public class ApplicationsController: ControllerBase
{
    private readonly IApplicationService _applicationService;
    public ApplicationsController(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(VacancyApplication),200)]
    public async Task<IActionResult> GetApplication([FromQuery] Guid id)
    {
        var res = await _applicationService.GetApplicationByIdAsync(id);
        return Ok(res);
    }

    [HttpGet("byvacancy")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetApplicationsByVacancy([FromQuery] Guid vacancyId)
    {
        var res = await _applicationService.GetApplicationsByVacancyIdAsync(vacancyId);
        return Ok(res);
    }
    
    [HttpPost("create")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> CreateApplication([FromBody] CreateApplicationsRequest application)
    {
        var res = await _applicationService.CreateApplicationAsync(new VacancyApplication()
        {
            ApplicationDate = DateTime.Now.ToUniversalTime(),
            ApplicantId = application.UserId,
            VacancyId = application.VacancyId,
            Status = ApplicationStatus.Received,
        });
        return Ok(res);
    }
    
    [HttpPut("update")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> UpdateApplication([FromBody] UpdateApplicationRequest application)
    {
        await _applicationService.UpdateApplicationAsync(new VacancyApplication()
        {
            Status = application.Status,
            Id = application.ApplicationId,
            VacancyId = application.VacancyId
        }, application.EmployerId);
        return Ok( new {Status = true});
    }
    
    [HttpDelete]
    [ProducesResponseType(200)]
    public async Task<IActionResult> DeleteApplication([FromBody] DeleApplicationRequest deleApplicationRequest)
    {
        await _applicationService.DeleteApplicationAsync(deleApplicationRequest.ApplicationId,deleApplicationRequest.UserId);
        return Ok();
    }
}