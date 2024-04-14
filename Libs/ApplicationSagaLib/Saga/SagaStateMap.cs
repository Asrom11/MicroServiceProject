using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Services;


public class CreateApplicationStateMap : SagaClassMap<ApplicationCreateState>
{
    protected override void Configure(EntityTypeBuilder<ApplicationCreateState> entity, ModelBuilder model)
    {
        base.Configure(entity, model);
        entity.Property(x => x.CurrentState);
    }
}