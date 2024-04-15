using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using Services;

namespace Infrastucture;

public sealed class SagasDbContext : SagaDbContext
{
    public SagasDbContext(DbContextOptions<SagasDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override IEnumerable<ISagaClassMap> Configurations => new[]
    {
        new CreateApplicationStateMap()
    };
}
