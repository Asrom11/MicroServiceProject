

using ExampleCore.HttpLogic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ProfileConnectionLib;
using ProfileConnectionLib.ConnectionServices;
using ProfileConnectionLib.ConnectionServices.Interfaces;
using Services.Interfaces;
using Services.Services;

public static class ServiceStartUp
{
    public static IServiceCollection TryAddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.TryAddScoped<IApplicationService, ApplicationService>();
        serviceCollection.TryAddScoped<IProfileConnectionServcie, ProfileConnectionService>();
        serviceCollection.AddHttpRequestService();
        return serviceCollection;
    }
    
}