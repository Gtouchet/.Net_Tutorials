namespace BackgroundTasks;

internal sealed class PeriodicTimerBackgroundTask : BackgroundTask
{
    private readonly PeriodicTimer timer;

    public PeriodicTimerBackgroundTask(TimeSpan taskTimeInterval) : base(taskTimeInterval)
    {
        this.timer = new PeriodicTimer(taskTimeInterval);
    }

    protected override async Task StartAsyncTask()
    {
        try
        {
            DateTime lastTaskExecutionTime = DateTime.Now;
            while (await this.timer.WaitForNextTickAsync(this.cancellationToken.Token))
            {
                Console.WriteLine(
                    $"\nNew task's execution\n" +
                    $"Time since last execution : {(DateTime.Now - lastTaskExecutionTime).TotalSeconds} s");
                lastTaskExecutionTime = DateTime.Now;

                await this.CallDatabase();
            }
        }
        catch (OperationCanceledException)
        {

        }
    }
}
