using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Dapr.Client;

var fileBindingName = "files";

using var daprClient = new DaprClientBuilder().Build();

Console.WriteLine("Creating file...");

var data = "Hello World!";
var metadata = new Dictionary<string, string>()
{
    { "fileName", "myTestFile.txt" }
};

await daprClient.InvokeBindingAsync(bindingName: fileBindingName, operation: "create", data: data, metadata: metadata );

Console.WriteLine("Finished creating file");