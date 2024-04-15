using Api.Controllers.Vacancy;
using MassTransit;

namespace Api;

public static class BrokerStartUp
{
    public static IServiceCollection TryAddBrokerForSaga(this IServiceCollection serviceCollection,
        IConfigurationManager configurationManager)
    {
        serviceCollection.AddMassTransit(configurator =>
        {
            configurator.SetKebabCaseEndpointNameFormatter();
            configurator.AddDelayedMessageScheduler();

            configurator.AddConsumer<VacancyResponseCounterConsumer>();
            
            configurator.UsingRabbitMq((brc, rbfc) =>
            {
                rbfc.UseInMemoryOutbox();
                rbfc.UseMessageRetry(r => { r.Incremental(3, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(1)); });

                rbfc.UseDelayedMessageScheduler();
                rbfc.Host("localhost", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                rbfc.ConfigureEndpoints(brc);
            });
        });
        return serviceCollection;
    }
}
