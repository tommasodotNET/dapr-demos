using Dapr.Actors;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddActors(options =>
{
    options.Actors.RegisterActor<DemoActor>();
});

var app = builder.Build();

app.MapActorsHandlers();

app.Run();
