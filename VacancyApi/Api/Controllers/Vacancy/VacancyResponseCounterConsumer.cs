using MassTransit;
using Services.Contracts.Vacancy;
using Services.Interfaces;

namespace Api.Controllers.Vacancy;

public class VacancyResponseCounterConsumer: 
    IConsumer<IVacancyIncrementRequest>
{
    private readonly IVacancyService _vacancyService;
    
    public VacancyResponseCounterConsumer(IVacancyService vacancyService)
    {
        _vacancyService = vacancyService;
    }
    
    public async Task Consume(ConsumeContext<IVacancyIncrementRequest> context)
    {
        await _vacancyService.IncrementVacancyAsync(context.Message.Id);
        await context.RespondAsync<IVacancyIncrementResponse>(new { Id = context.Message.Id });
    }
}