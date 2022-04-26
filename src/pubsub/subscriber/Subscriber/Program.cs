using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDaprClient();

var app = builder.Build();

app.UseCloudEvents();
app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapSubscribeHandler());

app.MapPost("/message", ([FromBody] string message) => 
{
    Console.WriteLine($"Received message {message} on channel.");
    return new OkResult();
}).WithTopic("pubsubdemo", "messages");

app.Run();
