# Dapr PubSub Component Demo

This demo shows how to use the Dapr PubSub component.

## Run Demo

Run the following command to start the demo:

Linux:
1. Start publisher:
```bash
./start-publisher.sh
```
2. Start subscriber:
```bash
./start-subscriber.sh
```
3. Start invoker:
```bash
./start-invoker.sh
```

Windows:
```powershell
./start-binding.ps1
```

## Demo Output

Once all the services are running, use the `sampleRequests.http` file to send request to the Publisher service.

The Publisher will log the request and the output will show
```
== APP == Requested greetings for <NAME>.
```

The Subscriber will handle the request and the output will show
```
== APP == Hi <NAME>!
```

Make sure to show how the DaprClient is injected using DI. Also show how to map the subscribed endpoints in the Subscriber service using:
```
app.UseCloudEvents();

app.UseEndpoints(endpoints => endpoints.MapSubscribeHandler());
```