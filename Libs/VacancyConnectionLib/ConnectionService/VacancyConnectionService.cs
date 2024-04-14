using CoreLib.HttpServiceV2.Services.Interfaces;
using ExampleCore.HttpLogic.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VacancyConnectionLib.ConnectionService.DtoModels.CheckVacancyExists;
using VacancyConnectionLib.ConnectionService.Interfaces;

namespace VacancyConnectionLib.ConnectionService;

public class VacancyConnectionService: IVacancyConnectionService
{
    private readonly IHttpRequestService _httpClientFactory;
    
    public VacancyConnectionService(IConfiguration configuration, IServiceProvider serviceProvider)
    { 
        var connectionType = configuration.GetSection("ConnectionSettings")["Type"];
        _httpClientFactory = serviceProvider.GetKeyedService<IHttpRequestService>(connectionType) ?? throw new InvalidOperationException();
    }

    public async Task CheckVacancyExistAsycn(CheckVacncyExistApiRequest vacancy)
    {
        var requestData = new HttpRequestData()
        {
            Uri = new Uri("http://localhost:5097/api/vacancy/exist"),
            Method = HttpMethod.Post,
            ContentType = ContentType.ApplicationJson,
            Body = vacancy,
        };
        
        var client = await _httpClientFactory.SendRequestAsync<CheckVacancyExistApiResponse,
            CheckVacncyExistApiRequest>(requestData);
        
    }
}