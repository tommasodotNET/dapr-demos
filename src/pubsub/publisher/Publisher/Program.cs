using Dapr.Client;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDaprClient();

var app = builder.Build();

app.MapGet("/publishmessage/{greetingName}", async (DaprClient daprClient, string greetingName) => 
{
    await daprClient.PublishEventAsync("pubsubdemo", "greetingName", greetingName);
    Console.WriteLine($"Requested greetings for {greetingName}.");

    return new OkResult();
});

app.Run();
