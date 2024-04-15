using Api.Controllers;
using Infrastucture;
using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using Services;

namespace Api;

public static class ApplicationSagaStartUp
{
    public static IServiceCollection TryAddApplicationSaga(this IServiceCollection serviceCollection, IConfigurationManager configurationManager)
    {
        var connectionString = "User ID=postgres;Password=123;Host=localhost;Port=5432;Database=applicationSaga;Pooling=true;";
        serviceCollection.AddDbContext<SagasDbContext>(options => options.UseNpgsql(connectionString));

        serviceCollection.AddMassTransit(cfg =>
        {
            cfg.SetKebabCaseEndpointNameFormatter();
            cfg.AddDelayedMessageScheduler();

            cfg.AddConsumer<ApplicationCreatorConsumer>();
            cfg.AddSagaStateMachine<ApplicationStateMachine, ApplicationCreateState>()
                .EntityFrameworkRepository(cfg =>
                {
                    cfg.ConcurrencyMode = ConcurrencyMode.Pessimistic;
                    cfg.ExistingDbContext<SagasDbContext>();
                    cfg.LockStatementProvider = new SqliteLockStatementProvider();
                });

            cfg.UsingRabbitMq((context, cfg) =>
            {
                cfg.UseInMemoryOutbox();
                cfg.UseMessageRetry(r =>
                {
                    r.Incremental(3, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
                });
                cfg.UseDelayedMessageScheduler();

                cfg.Host("localhost", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.ConfigureEndpoints(context);
            });
        }); 
        return serviceCollection;
    }
}