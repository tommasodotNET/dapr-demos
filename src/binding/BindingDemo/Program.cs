using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Dapr.Client;

var sqlBindingName = "sqldb";

using var daprClient = new DaprClientBuilder().Build();

Console.WriteLine("Processing batch..");

string jsonFile = File.ReadAllText("../orders.json");
var ordersArray = JsonSerializer.Deserialize<Orders>(jsonFile);

foreach(Order ord in ordersArray?.orders ?? new Order[] {}){
    var sqlText = $"insert into orders (orderid, customer, price) values ({ord.OrderId}, '{ord.Customer}', {ord.Price});";
    var command = new Dictionary<string,string>(){
        {"sql",
        sqlText}
    };

    // Insert order using Dapr output binding via Dapr Client SDK
    await daprClient.InvokeBindingAsync(bindingName: sqlBindingName, operation: "exec", data: "", metadata: command);
}

Console.WriteLine("Finished processing batch");

public record Order([property: JsonPropertyName("orderid")] int OrderId, [property: JsonPropertyName("customer")] string Customer, [property: JsonPropertyName("price")] float Price);
public record Orders([property: JsonPropertyName("orders")] Order[] orders);