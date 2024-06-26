﻿using Domain.Interfaces;
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
        serviceCollection.TryAddScoped<ICheckUser, CheckUser>();
        serviceCollection.TryAddScoped<IStoreVacancy, VacancyRepository>();
        serviceCollection.TryAddScoped<IStoreFeedback,VacancyFeedbackRepository>();
        
        var connectionString = configurationManager.GetConnectionString("DefaultConnection");
        serviceCollection.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));
        serviceCollection.AddScoped<DbContext>(provider => provider.GetService<ApplicationDbContext>());
        return serviceCollection;
    }
}