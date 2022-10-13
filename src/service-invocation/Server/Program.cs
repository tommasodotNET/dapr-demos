using MyModel;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => 
{
    var message = "Hello Dapr!";
    return Results.Json(message);
});

app.MapGet("/hello/{name}", (string name) => 
{
    var message = $"Hello {name}!";
    return Results.Json(message);
});

app.MapPost("/registerUser", (UserAccount myModel) => 
{
    myModel.Id = Guid.NewGuid().ToString();

    return myModel;
});

app.Run();
