using ExampleCore.HttpLogic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ProfileConnectionLib.ConnectionServices;
using ProfileConnectionLib.ConnectionServices.Interfaces;

namespace ProfileConnectionLib;

public static class ProfileLibStartUp
{
    public static IServiceCollection TryAddProfileLib(this IServiceCollection serviceCollection)
    {
        serviceCollection.TryAddScoped<IProfileConnectionServcie, ProfileConnectionService>();
        serviceCollection.AddHttpRequestService();
        return serviceCollection;
    }
}