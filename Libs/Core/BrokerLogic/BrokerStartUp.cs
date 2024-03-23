using CoreLib.HttpServiceV2.Services.Interfaces;
using ExampleCore.BrokerLogic.Services;
using ExampleCore.HttpLogic.Services;
using ExampleCore.HttpLogic.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.ObjectPool;
using RabbitMQ.Client;

namespace ExampleCore.BrokerLogic;

public static class HttpServiceStartup
{

    public static IServiceCollection AddBrokerLogic(this IServiceCollection services)
    {
        services.TryAddSingleton<IPooledObjectPolicy<IConnection>>(_ =>
        {
            return new ConnectionPooledPolicy("localhost");
        });
        
        services.TryAddSingleton<ObjectPool<IConnection>>(serviceProvider =>
        {
            var policy = serviceProvider.GetRequiredService<IPooledObjectPolicy<IConnection>>();
            return new DefaultObjectPool<IConnection>(policy);
        });
        services.TryAddKeyedTransient<IHttpRequestService, ProducerRequestService>("broker");
        return services;
    }
}