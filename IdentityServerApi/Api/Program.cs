using Dal;
using ExampleCore.BrokerLogic.Consumer;
using ExampleCore.BrokerLogic.Consumer.Interfaces;
using IdentityServerDal;
using IdentityServerLogic;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ProfileConnectionLib.ConnectionServices.DtoModels.CheckUserExists;
using ProfileConnectionLib.ConnectionServices.DtoModels.UserNameLists;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.TryAddDal();
builder.Services.TryAddLogic();
builder.Services.TryAddApplicationContext(builder.Configuration);
builder.Services.TryAddSingleton<IConsumer<CheckUserExistProfileApiRequest>, UserExistConsumer>();
builder.Services.TryAddSingleton<IConsumer<UserNameListProfileApiRequest>, UserNameListConsumer>();

builder.Services.AddHostedService<BasicBackgroundConsumer<CheckUserExistProfileApiRequest>>(provider =>
{
    var queueName = "rpc/http://localhost:5097/api/user/exist";
    var consumer = provider.GetRequiredService<IConsumer<CheckUserExistProfileApiRequest>>();
    return new BasicBackgroundConsumer<CheckUserExistProfileApiRequest>(consumer, queueName);
});

builder.Services.AddHostedService<BasicBackgroundConsumer<UserNameListProfileApiRequest>>(provider =>
{
    var queueName = "rpc/http://localhost:5097/api/user/namelist";
    var consumer = provider.GetRequiredService<IConsumer<UserNameListProfileApiRequest>>();
    
    return new BasicBackgroundConsumer<UserNameListProfileApiRequest>(consumer, queueName);
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