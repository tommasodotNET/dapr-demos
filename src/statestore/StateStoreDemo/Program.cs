using Dapr.Client;

using var daprClient = new DaprClientBuilder().Build();

var initialState = 0;
Console.WriteLine("Saving initial state...");
await daprClient.SaveStateAsync("statestore", "counter", initialState);

Console.WriteLine("Getting state entry...");
var stateEntry = await daprClient.GetStateEntryAsync<int>("statestore", "counter");
Console.WriteLine($"Counter = {stateEntry.Value}");

Console.WriteLine("Incrementing state entry...");
stateEntry.Value++;

Console.WriteLine("Saving state entry...");
await stateEntry.SaveAsync();

Console.WriteLine("Getting state value...");
var newStateValue = await daprClient.GetStateAsync<int>("statestore", "counter");
Console.WriteLine($"Counter = {newStateValue}");

Console.WriteLine("Deleting state...");
await daprClient.DeleteStateAsync("statestore", "counter");