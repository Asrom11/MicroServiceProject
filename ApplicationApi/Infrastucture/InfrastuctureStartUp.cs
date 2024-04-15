// See https://aka.ms/new-console-template for more information

using Domain.Interfaces;
using Infrastucture.Connection;
using Infrastucture.Data;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Infrastucture;
public static class InfrastuctureStartUp
{
    public static IServiceCollection TryAddInfrastucture(this IServiceCollection serviceCollection, IConfigurationManager configurationManager)
    {
        var connectionString = configurationManager.GetConnectionString("DefaultConnection");
        serviceCollection.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));
        serviceCollection.AddScoped<DbContext>(provider => provider.GetService<ApplicationDbContext>());
        
        serviceCollection.TryAddScoped(typeof(IStandartStore<>), typeof(BaseRepository<>));
        serviceCollection.TryAddScoped<ICheckVacancy, CheckVacancy>();
        serviceCollection.TryAddScoped<ICheckUser, CheckUser>();
        serviceCollection.TryAddScoped<IStoreApplication, ApplicationRepository>();
        serviceCollection.TryAddVacancyLib();
        


        return serviceCollection;
    }
}