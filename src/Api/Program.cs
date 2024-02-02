using Api.Extensions;
using Application;
using Infrastructure;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddConfiguredDatabase(builder.Configuration);
builder.Services.AddMemoryCache();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddPersistence();
builder.Services.AddConfiguredSwagger();
builder.Services.RunMigrations();

var app = builder.Build();

app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.RunSeeder();

var serviceName = typeof(Program).Namespace?.Split('.')[0];
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{env}.json")
    .AddEnvironmentVariables()
    .Build();

try
{

    var address = configuration.GetValue<string>("urls");

    Console.WriteLine($"Starting  on {address}");
    app.Run();
    return 0;
}
catch (Exception)
{
    Console.WriteLine($"Terminated {serviceName} host unexpectedly!");
    return 1;
}
finally
{
}