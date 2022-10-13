// The following example showcases a few features of Actors
//
// Every actor should inherit from the Actor type, and must implement one or more actor interfaces.
// In this case the actor interfaces are IDemoActor and IBankActor.
// 
// For Actors to use Reminders, it must derive from IRemindable.
// If you don't intend to use Reminder feature, you can skip implementing IRemindable and reminder 
// specific methods which are shown in the code below.
using System.Text.Json;
using Dapr.Actors.Runtime;
using IDemoActorInterface;

public class DemoActor : Actor, IDemoActor, IRemindable
{
    private const string StateName = "my_data";

    public DemoActor(ActorHost host)
        : base(host)
    {
    }

    public async Task SaveData(MyData data)
    {
        Console.WriteLine($"This is Actor id {this.Id} with data {data}.");

        // Set State using StateManager, state is saved after the method execution.
        await this.StateManager.SetStateAsync<MyData>(StateName, data);
    }

    public Task<MyData> GetData()
    {
        // Get state using StateManager.
        return this.StateManager.GetStateAsync<MyData>(StateName);
    }

    public Task TestThrowException()
    {
        throw new NotImplementedException();
    }

    public Task TestNoArgumentNoReturnType()
    {
        return Task.CompletedTask;
    }

    public async Task RegisterReminder()
    {
        await this.RegisterReminderAsync("TestReminder", null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));
    }

    public Task UnregisterReminder()
    {
        return this.UnregisterReminderAsync("TestReminder");
    }

    public async Task ReceiveReminderAsync(string reminderName, byte[] state, TimeSpan dueTime, TimeSpan period)
    {
        // This method is invoked when an actor reminder is fired.
        var actorState = await this.StateManager.GetStateAsync<MyData>(StateName);
        actorState.PropertyB = $"Reminder triggered at '{DateTime.Now:yyyy-MM-ddTHH:mm:ss}'";
        await this.StateManager.SetStateAsync<MyData>(StateName, actorState);
    }

    class TimerParams
    {
        public int IntParam { get; set; }
        public string? StringParam { get; set; }
    }

    /// <inheritdoc/>
    public Task RegisterTimer()
    {
        var timerParams = new TimerParams
        {
            IntParam = 100,
            StringParam = "timer test",
        };

        var serializedTimerParams = JsonSerializer.SerializeToUtf8Bytes(timerParams);
        return this.RegisterTimerAsync("TestTimer", nameof(this.TimerCallback), serializedTimerParams, TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(3));
    }

    public Task UnregisterTimer()
    {
        return this.UnregisterTimerAsync("TestTimer");
    }

    // This method is called whenever an actor is activated.
    // An actor is activated the first time any of its methods are invoked.
    protected override Task OnActivateAsync()
    {
        // Provides opportunity to perform some optional setup.
        return Task.CompletedTask;
    }

    // This method is called whenever an actor is deactivated after a period of inactivity.
    protected override Task OnDeactivateAsync()
    {
        // Provides Opportunity to perform optional cleanup.
        return Task.CompletedTask;
    }

    /// <summary>
    /// This method is called when the timer is triggered based on its registration.
    /// It updates the PropertyA value.
    /// </summary>
    /// <param name="data">Timer input data.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task TimerCallback(byte[] data)
    {
        var state = await this.StateManager.GetStateAsync<MyData>(StateName);
        state.PropertyA = $"Timer triggered at '{DateTime.Now:yyyyy-MM-ddTHH:mm:ss}'";
        await this.StateManager.SetStateAsync<MyData>(StateName, state);
        var timerParams = JsonSerializer.Deserialize<TimerParams>(data);
        Console.WriteLine("Timer parameter1: " + timerParams?.IntParam);
        Console.WriteLine("Timer parameter2: " + timerParams?.StringParam);
    }
}