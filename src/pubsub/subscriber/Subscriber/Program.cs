using Dapr.Client;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDaprClient();

var app = builder.Build();

app.UseCloudEvents();
app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapSubscribeHandler());

app.MapGet("/probe", () => new OkResult());

app.MapGet("/", async (DaprClient daprClient) => await daprClient.GetStateAsync<string>("statestore", "greetingname"));

app.MapPost("/greetingName", async (DaprClient daprClient, [FromBody] string greetingName) => 
{
    Console.WriteLine($"Hi {greetingName}!");
    await daprClient.SaveStateAsync("statestore", "greetingname", greetingName);
    return new OkResult();
}).WithTopic("pubsubdemo", "greetingname");

app.Run();
