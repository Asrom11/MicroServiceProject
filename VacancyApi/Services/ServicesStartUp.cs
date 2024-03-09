// See https://aka.ms/new-console-template for more information


using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ProfileConnectionLib.ConnectionServices;
using ProfileConnectionLib.ConnectionServices.Interfaces;
using Services.Interfaces;
using Services.Services;

public static class ServiceStartUp
{
    public static IServiceCollection TryAddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.TryAddScoped<IApplicationService, ApplicationService>();
        serviceCollection.TryAddScoped<IVacancyService, VacancyService>();
        serviceCollection.TryAddScoped<IFeedbackService, FeedBackService>();
        serviceCollection.TryAddScoped<IProfileConnectionServcie, ProfileConnectionService>();
        return serviceCollection;
    }
    
}