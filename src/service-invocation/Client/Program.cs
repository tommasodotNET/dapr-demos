using System.Net.Http.Json;
using Dapr.Client;
using MyModel;

Console.WriteLine("============ HTTP Client ============");
Console.WriteLine("Calling using HttpClient...");
await ReadUsingHttpClientAsync();
Console.WriteLine("Done.");
Console.WriteLine("=====================================");

Console.WriteLine();

Console.WriteLine("========= Dapr HTTP Client ==========");
Console.WriteLine("Calling using DaprHttpClient...");
await ReadUsingHttpDaprClientAsync();
Console.WriteLine("Done.");
Console.WriteLine("=====================================");

Console.WriteLine();

Console.WriteLine("=========== Dapr Client ============");
Console.WriteLine("Calling using DaprClient...");
await ReadUsingDaprClientAsync();
Console.WriteLine("Done.");
Console.WriteLine("====================================");

async Task ReadUsingHttpClientAsync()
{
    using var httpClient = new HttpClient();

    var daprPort = Environment.GetEnvironmentVariable("DAPR_HTTP_PORT") ?? "3500";
    Console.WriteLine($"DAPR_HTTP_PORT: {daprPort}");
    var baseUrl = $"http://localhost:{daprPort}/v1.0";
    var appId = "server";

    var helloResponse = new HttpResponseMessage();
    var helloMessage = "";

    helloResponse = await httpClient.GetAsync($"{baseUrl}/invoke/{appId}/method/");
    helloResponse.EnsureSuccessStatusCode();
    helloMessage = await helloResponse.Content.ReadAsStringAsync();
    Console.WriteLine(helloMessage);

    var name = "Tommaso";
    helloResponse = await httpClient.GetAsync($"{baseUrl}/invoke/{appId}/method/hello/{name}");
    helloResponse.EnsureSuccessStatusCode();
    helloMessage = await helloResponse.Content.ReadAsStringAsync();
    Console.WriteLine(helloMessage);

    var data = new UserAccount(){ Name = "Tommaso" };
    var registerUserResponse = await httpClient.PostAsJsonAsync($"{baseUrl}/invoke/{appId}/method/registerUser", data);
    registerUserResponse.EnsureSuccessStatusCode();
    var userAccount = await registerUserResponse.Content.ReadFromJsonAsync<UserAccount>();
    if(userAccount != null)
    {
        Console.WriteLine(userAccount.ToString());
    }
}

async Task ReadUsingHttpDaprClientAsync()
{
    using var httpDaprClient = DaprClient.CreateInvokeHttpClient(appId: "server");

    var helloResponse = new HttpResponseMessage();
    var helloMessage = "";

    helloResponse = await httpDaprClient.GetAsync("");
    helloResponse.EnsureSuccessStatusCode();
    helloMessage = await helloResponse.Content.ReadAsStringAsync();
    Console.WriteLine(helloMessage);

    var name = "Tommaso";
    helloResponse = await httpDaprClient.GetAsync($"hello/{name}");
    helloResponse.EnsureSuccessStatusCode();
    helloMessage = await helloResponse.Content.ReadAsStringAsync();
    Console.WriteLine(helloMessage);

    var data = new UserAccount(){ Name = "Tommaso" };
    var registerUserResponse = await httpDaprClient.PostAsJsonAsync("registerUser", data);
    registerUserResponse.EnsureSuccessStatusCode();
    var userAccount = await registerUserResponse.Content.ReadFromJsonAsync<UserAccount>();
    if(userAccount != null)
    {
        Console.WriteLine(userAccount.ToString());
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