# Dapr Actor Component Demo

This demo shows how to use the Dapr actor component.

## Run Demo

Run the following command to start the demo:

Linux:
1. Start actor:
```bash
./start-actor.sh
```

2. Start client:
```bash
./start-client.sh
```

Windows:
1. Start actor:
```bash
./start-actor.ps1
```

2. Start client:
```bash
./start-client.ps1
```

## Demo Output

First, focus on the folder structure:
- ActorClient: simple Console App to invoke actor methods
- IDemoActor: Class Library containing the actor interface and the shared Model
- DemoActor: Web Application that registers the actor and implements the actor methods. The actor is implemented in `Program.cs` using
```csharp
builder.Services.AddActors(options =>
{
    options.Actors.RegisterActor<DemoActor>();
});
```

Make sure to show logic for Timer and Reminder implementation in the Actor service:
- `TimerCallback` will upate PropertyA of the actor state to show current time
- `ReminderCallback` will upate PropertyB of the actor state to show current time

This is done so that we can read those property from the client and show how the two different triggers work.

Once the services are running, focus on the output of the Client service. 

The first lines will invoke the actor in different ways, using Remoting:
```csharp
var proxy = ActorProxy.Create<IDemoActor>(actorId, "DemoActor");
```

1. The first call will save datas in the actor state.
2. The second call will retrieve the data from the actor state. The received datas will be printed in the console.
3. The third call will invoke a not implemented method ensuring to receive the correct exception. You can check the output to verify that the exception is due to a NotImplemented method.

Now we invoke the actor using NonRemoting:
```csharp
var nonRemotingProxy = ActorProxy.Create(actorId, "DemoActor");
```

1. The first call will save same data as before.
2. The second call will retrieve the data from the actor state. The received datas won't be printed now.
3. The third call will register a timer.
4. The fourth call will register the reminder.

At this point the Client service will wait 4 seconds to make sure the timer and the reminder are triggered.

5. The fifth call will retrieve datas after the timer has been triggered. The output will show
    ```
    Received data is PropertyA: Timer triggered at <DATE_TIME>, PropertyB: ValueB
    ```
    Note that the value for PropertyB is the same as before.
6. Now we use a Timer on the Client service to track new values coming from the Reminder. The output will show
    ```
    Received data is PropertyA: Timer triggered at <DATE_TIME>, PropertyB: <DATE_TIME>
    ```
    Note that now the value of PropertyA is always the same, while the value for PropertyB is being updated every 5 seconds.