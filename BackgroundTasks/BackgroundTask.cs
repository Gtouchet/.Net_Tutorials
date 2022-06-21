namespace BackgroundTasks;

/// <summary>
/// Performs a repeating task <br/><br/>
/// The Legacy version does not respect the time between 2 executions of the task, because a call to an asynchronous operation will shift it <br/>
/// The version using a PeriodicTimer respects the time between 2 executions and is not impacted by a call to the asynchronous operation
/// </summary>
internal abstract class BackgroundTask
{
    protected readonly CancellationTokenSource cancellationToken;
    private readonly Random random;
    protected readonly TimeSpan taskTimeInterval;
    private Task timerTask;

    public BackgroundTask(TimeSpan taskTimeInterval)
    {
        this.cancellationToken = new CancellationTokenSource();
        this.random = new Random();
        this.taskTimeInterval = taskTimeInterval;
    }

    /// <summary>
    /// Starts the repeating task
    /// </summary>
    public void Start()
    {
        this.timerTask = this.StartAsyncTask();

        Console.WriteLine("Task started");
    }

    /// <summary>
    /// Loops and performs an asynchronous operation <br/>
    /// Calculates and displays the time between two task executions
    /// </summary>
    /// <returns></returns>
    protected abstract Task StartAsyncTask();

    /// <summary>
    /// Emulate a call to a database <br/>
    /// Operation's time is random
    /// </summary>
    /// <returns></returns>
    protected async Task CallDatabase()
    {
        int databaseTaskTimeMs = this.random.Next(0, (int)this.taskTimeInterval.TotalMilliseconds);
        await Task.Delay(databaseTaskTimeMs);

        Console.WriteLine($"Database operation done after {databaseTaskTimeMs} ms");
    }

    /// <summary>
    /// Properly stops the repeating task
    /// </summary>
    /// <returns></returns>
    public async Task Stop()
    {
        if (this.timerTask == null)
        {
            return;
        }

        this.cancellationToken.Cancel();
        await this.timerTask;
        this.cancellationToken.Dispose();

        Console.WriteLine("Task stopped");
    }
}
