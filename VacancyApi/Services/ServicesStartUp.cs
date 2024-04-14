using ExampleCore.HttpLogic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ProfileConnectionLib;

using Services.Interfaces;
using Services.Services;
using VacancyConnectionLib.ConnectionService;
using VacancyConnectionLib.ConnectionService.Interfaces;

public static class ServiceStartUp
{
    public static IServiceCollection TryAddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.TryAddScoped<IVacancyService, VacancyService>();
        serviceCollection.TryAddScoped<IFeedbackService, FeedBackService>();
        serviceCollection.AddHttpRequestService();
        serviceCollection.TryAddProfileLib();
        return serviceCollection;
    }
    
}