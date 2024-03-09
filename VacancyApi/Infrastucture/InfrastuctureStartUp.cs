using Domain.Interfaces;
using Infrastucture.Connections;
using Infrastucture.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Protocols;
using ProfileConnectionLib;
using Services.Interfaces;

namespace Infrastucture;

public static class InfrastuctureStartUp
{
    public static IServiceCollection TryAddInfrastucture(this IServiceCollection serviceCollection, IConfigurationManager configurationManager)
    {
        serviceCollection.TryAddScoped(typeof(IStandartStore<>), typeof(BaseRepository<>));
        serviceCollection.TryAddScoped<IChekUser, CheckUser>();
        serviceCollection.TryAddScoped<IStoreApplication, ApplicationRepository>();
        serviceCollection.TryAddScoped<IStoreFeedback, VacancyFeedbackRepository>();
        serviceCollection.TryAddScoped<IStoreVacancy, VacancyRepository>();
        serviceCollection.TryAddProfileLib();
        
        var connectionString = configurationManager.GetConnectionString("DefaultConnection");
        serviceCollection.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));
        return serviceCollection;
    }
    
    public static IServiceCollection TryAddApplicationContext(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<ApplicationDbContext>();
        return serviceCollection;
    }
}