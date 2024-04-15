using Api;
using Infrastucture;
using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration;
using Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.TryAddServices();

builder.Services.TryAddInfrastucture(builder.Configuration);

builder.Services.TryAddApplicationSaga(builder.Configuration);
var app = builder.Build();


    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();
app.MapControllers();
app.Run();
