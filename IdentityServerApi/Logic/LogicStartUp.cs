using IdentityServerLogic.Roles;
using IdentityServerLogic.Roles.Interfaces;
using IdentityServerLogic.Users;
using IdentityServerLogic.Users.Interfaces;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;


namespace IdentityServerLogic;

public static class LogicStartUp
{
    public static IServiceCollection TryAddLogic(this IServiceCollection serviceCollection)
    {
        serviceCollection.TryAddScoped<IRoleLogicManager,RoleLogicManager>();
        serviceCollection.TryAddScoped<IUserLogicManager,UserLogicManager>();
        serviceCollection.AddMassTransit(x =>
        {
            x.AddConsumer<CheckUserExistConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
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