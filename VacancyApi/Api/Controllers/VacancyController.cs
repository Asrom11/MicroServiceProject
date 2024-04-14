using Api.Controllers.Vacancy.Requests;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using VacancyConnectionLib.ConnectionService.DtoModels.CheckVacancyExists;

namespace Api.Controllers;


[Route("api/vacancy")]
[ApiController]
public class VacancyController : ControllerBase
{

    private readonly IVacancyService _vacancyService;

    public VacancyController(IVacancyService vacancyService)
    {
        _vacancyService = vacancyService;
    }

    [HttpPost("create")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> CreateVacancyAsync([FromBody] CreateVacancyRequest createVacancy)
    {
        var res = await _vacancyService.CreateVacancyAsync(new Domain.Entities.Vacancy()
        {
            VacancyStatus = VacancyStatus.Open,
            Description = createVacancy.Description,
            EmployerId = createVacancy.EmployerId,
            Title = createVacancy.Title
        });
        return Ok(res);
    }

    [HttpPut("update")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> UpdateVacancyAsync([FromBody] UpdateVacancyRequest updateVacancy)
    {
        await _vacancyService.UpdateVacancyAsync(new Domain.Entities.Vacancy()
        {
            VacancyStatus = updateVacancy.VacancyStatus,
            Description = updateVacancy.Description,
            EmployerId = updateVacancy.EmployerId,
            Title = updateVacancy.Title,
            Id = updateVacancy.VacancyId
        });
        return Ok();
    }

    [HttpGet]
    [ProducesResponseType<List<Domain.Entities.Vacancy>>(200)]
    public async Task<IActionResult> GetVacancyListAsync()
    {
       var res = await _vacancyService.GetVacancyListAsync();
       return Ok(res);
    }

    
    [HttpPost("exist")]
    public async Task<IActionResult> CheckVacancyProfile([FromBody] CheckVacncyExistApiRequest vacncyExist)
    {
        var res = await _vacancyService.CheckVacancyExistAsync(vacncyExist.VacancyId);

        return Ok(new CheckVacancyExistApiResponse()
        {
            VacancyId = res
        });
    }
}