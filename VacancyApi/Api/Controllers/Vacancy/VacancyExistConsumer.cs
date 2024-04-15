using IdentityServerLogic;
using Newtonsoft.Json;
using Services.Interfaces;
using VacancyConnectionLib.ConnectionService.DtoModels.CheckVacancyExists;

namespace Api.Controllers.Vacancy;

public class VacancyExistConsumer: ConsumerBase<CheckVacncyExistApiRequest, CheckVacancyExistApiResponse>
{
    public VacancyExistConsumer(IServiceScopeFactory scopeFactory) : base(scopeFactory)
    {
    }

    protected override async Task<CheckVacancyExistApiResponse> ProcessMessage(CheckVacncyExistApiRequest message)
    {
        using (var scope = ScopeFactory.CreateScope())
        {
            
            var scopedService = scope.ServiceProvider.GetRequiredService<IVacancyService>();

            var id = await scopedService.CheckVacancyExistAsync(message.VacancyId);

            var res = new CheckVacancyExistApiResponse()
            {
                VacancyId = id
            };
            
            return res;
        }
    }

    protected override string SerializeResponse(CheckVacancyExistApiResponse response)
    {
        return JsonConvert.SerializeObject(response);
    }

    protected override string SerializeErrorResponse()
    {
        return JsonConvert.SerializeObject(new CheckVacancyExistApiResponse()
        {
            VacancyId = Guid.Empty
        });
    }
}