using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDaprClient();

var app = builder.Build();

app.UseCloudEvents();
app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapSubscribeHandler());

app.MapPost("/greetingName", async ([FromBody] string greetingName) => 
{
    Console.WriteLine($"Hi {greetingName}!");
    return new OkResult();
}).WithTopic("pubsub", "greetingname");

app.Run();
