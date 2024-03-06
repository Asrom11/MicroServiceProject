using IdentityServerLogic.Roles;
using IdentityServerLogic.Roles.Interfaces;
using IdentityServerLogic.Users;
using IdentityServerLogic.Users.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;


namespace IdentityServerLogic;

public static class LogicStartUp
{
    public static IServiceCollection TryAddLogic(this IServiceCollection serviceCollection)
    {
        serviceCollection.TryAddScoped<IRoleLogicManager,RoleLogicManager>();
        serviceCollection.TryAddScoped<IUserLogicManager,UserLogicManager>();
        return serviceCollection;
    }
}