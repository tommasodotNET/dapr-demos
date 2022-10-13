using System.Net.Http.Json;
using Dapr.Client;
using MyModel;

Console.WriteLine("Calling using DaprHttpClient...");
await ReadUsingHttpDaprClientAsync();
Console.WriteLine("Done.");

Console.WriteLine("Calling using DaprClient...");
await ReadUsingDaprClientAsync();
Console.WriteLine("Done.");

async Task ReadUsingHttpDaprClientAsync()
{
    var httpDaprClient = DaprClient.CreateInvokeHttpClient(appId: "server");

    var helloResponse = new HttpResponseMessage();
    helloResponse = await httpDaprClient.GetAsync("");
    if(helloResponse.IsSuccessStatusCode)
    {
        var helloMessage = await helloResponse.Content.ReadAsStringAsync();
        Console.WriteLine(helloMessage);
    }

    var name = "Tommaso";
    helloResponse = await httpDaprClient.GetAsync($"hello/{name}");
    if(helloResponse.IsSuccessStatusCode)
    {
        var helloMessage = await helloResponse.Content.ReadAsStringAsync();
        Console.WriteLine(helloMessage);
    }

    var data = new UserAccount(){ Name = "Tommaso" };
    var registerUserResponse = await httpDaprClient.PostAsJsonAsync("registerUser", data);
    if(registerUserResponse.IsSuccessStatusCode)
    {
        var userAccount = await registerUserResponse.Content.ReadFromJsonAsync<UserAccount>();
        if(userAccount != null)
        {
            Console.WriteLine(userAccount.ToString());
        }
    }
}

async Task ReadUsingDaprClientAsync()
{
    using var daprClient = new DaprClientBuilder().Build();

    var helloMessage = "";
    helloMessage = await daprClient.InvokeMethodAsync<string>(HttpMethod.Get, "server", "");
    Console.WriteLine(helloMessage);

    var name = "Tommaso";
    helloMessage = await daprClient.InvokeMethodAsync<string>(HttpMethod.Get, "server", "hello/" + name);
    Console.WriteLine(helloMessage);

    var data = new UserAccount(){ Name = "Tommaso" };
    var userAccount = await daprClient.InvokeMethodAsync<UserAccount, UserAccount>(HttpMethod.Post, "server", "registerUser", data);
    Console.WriteLine(userAccount.ToString());
}