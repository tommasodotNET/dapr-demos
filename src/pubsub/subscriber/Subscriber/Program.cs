using Dapr.Client;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDaprClient();

var app = builder.Build();

app.UseCloudEvents();
app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapSubscribeHandler());

app.MapGet("/", async (DaprClient daprClient) => await daprClient.GetStateAsync<string>("statestore", "greetingName"));

app.MapPost("/greetingName", async (DaprClient daprClient, [FromBody] string greetingName) => 
{
    Console.WriteLine($"Hi {greetingName}!");
    await daprClient.SaveStateAsync("statestore", "greetingName", greetingName);
    return new OkResult();
}).WithTopic("pubsubdemo", "messages");

app.Run();
