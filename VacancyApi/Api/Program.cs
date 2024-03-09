using Infrastucture;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.TryAddInfrastucture(builder.Configuration);
builder.Services.TryAddServices();
builder.Services.TryAddApplicationContext();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();