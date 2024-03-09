
using IdentityServerDal.Roles;
using IdentityServerDal.Roles.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IdentityServerDal;

public static class DalStartUp
{
    public static IServiceCollection TryAddDal(this IServiceCollection serviceCollection)
    {
        serviceCollection.TryAddScoped<IUserRepository,UserRepository>();
        serviceCollection.TryAddScoped<IRoleRepository, RoleRepository>();
        return serviceCollection;
    }
    
    public static IServiceCollection TryAddApplicationContext(this IServiceCollection serviceCollection, IConfigurationManager configurationManager)
    {
        var connectionString = configurationManager.GetConnectionString("DefaultConnection");
        serviceCollection.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));
        return serviceCollection;
    }
}