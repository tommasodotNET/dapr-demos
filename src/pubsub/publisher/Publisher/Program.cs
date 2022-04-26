using Dapr.Client;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDaprClient();

var app = builder.Build();

app.MapGet("/publishmessage", async (DaprClient daprClient) => 
{
    var message = "Hello from publisher";
    await daprClient.PublishEventAsync("pubsubdemo", "messages", message);
    Console.WriteLine($"Published message {message} on channel.");
});

app.Run();
