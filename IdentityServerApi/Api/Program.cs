using Dal;
using ExampleCore.BrokerLogic.Consumer;
using ExampleCore.BrokerLogic.Consumer.Interfaces;
using IdentityServerDal;
using IdentityServerLogic;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ProfileConnectionLib.ConnectionServices.DtoModels.CheckUserExists;
using ProfileConnectionLib.ConnectionServices.DtoModels.UserNameLists;
using MassTransit;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.TryAddDal();
builder.Services.TryAddLogic();
builder.Services.TryAddApplicationContext(builder.Configuration);
builder.Services.TryAddSingleton<ExampleCore.BrokerLogic.Consumer.Interfaces.IConsumer<CheckUserExistProfileApiRequest>, UserExistConsumer>();
builder.Services.TryAddSingleton<ExampleCore.BrokerLogic.Consumer.Interfaces.IConsumer<UserNameListProfileApiRequest>, UserNameListConsumer>();

builder.Services.AddHostedService<BasicBackgroundConsumer<CheckUserExistProfileApiRequest>>(provider =>
{
    var queueName = "rpc/http://localhost:5097/api/user/exist";
    var consumer = provider.GetRequiredService<ExampleCore.BrokerLogic.Consumer.Interfaces.IConsumer<CheckUserExistProfileApiRequest>>();
    return new BasicBackgroundConsumer<CheckUserExistProfileApiRequest>(consumer, queueName);
});

builder.Services.AddHostedService<BasicBackgroundConsumer<UserNameListProfileApiRequest>>(provider =>
{
    var queueName = "rpc/http://localhost:5097/api/user/namelist";
    var consumer = provider.GetRequiredService<ExampleCore.BrokerLogic.Consumer.Interfaces.IConsumer<UserNameListProfileApiRequest>>();
    
    return new BasicBackgroundConsumer<UserNameListProfileApiRequest>(consumer, queueName);
});

builder.Services.AddMassTransit(configurator =>
{
    configurator.SetKebabCaseEndpointNameFormatter();
    configurator.AddDelayedMessageScheduler();

    configurator.AddConsumer<UserResponseCounterConsumer>();

    configurator.UsingRabbitMq((brc, rbfc) =>
    {
        rbfc.UseMessageRetry(r =>
        {

            r.Incremental(1, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(1));
        });

        rbfc.UseDelayedMessageScheduler();
        
        rbfc.Host("localhost", "/", h =>
            {
                h.Username("guest");
                h.Password("guest");
            });

        rbfc.ConfigureEndpoints(brc);
    });
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();