using Domain.Entities;
using MassTransit;
using Services.Contracts;
using Services.Contracts.Application;
using Services.Interfaces;

namespace Api.Controllers;

public class ApplicationCreatorConsumer: IConsumer<CreateApplicationRequest>
{

    private readonly IApplicationService _applicationService;
    public ApplicationCreatorConsumer(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }
    
    public async Task Consume(ConsumeContext<CreateApplicationRequest> context)
    {
        var res = await _applicationService.CreateApplicationAsync(new VacancyApplication()
        {
            ApplicationDate = DateTime.Now.ToUniversalTime(),
            ApplicantId = context.Message.UserId,
            VacancyId = context.Message.VacancyId,
            Status = ApplicationStatus.Received
        });
        
        await context.RespondAsync<CreateApplicationResponse>(new CreateApplicationResponse
        {
            Id = res,
            ErrorMessage = null
        });
    }
}