using ExampleCore.BrokerLogic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using VacancyConnectionLib.ConnectionService;
using VacancyConnectionLib.ConnectionService.Interfaces;

public static class VacancyLibStartUp
{
    public static IServiceCollection TryAddVacancyLib(this IServiceCollection serviceCollection)
    {
       serviceCollection.TryAddScoped<IVacancyConnectionService, VacancyConnectionService>();
       serviceCollection.AddBrokerLogic();
        return serviceCollection;
    }
}