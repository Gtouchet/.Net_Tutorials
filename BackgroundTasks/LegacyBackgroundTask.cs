namespace BackgroundTasks;

internal sealed class LegacyBackgroundTask : BackgroundTask
{
    public LegacyBackgroundTask(TimeSpan taskTimeInterval) : base(taskTimeInterval)
    {
        
    }

    protected override async Task StartAsyncTask()
    {
        DateTime lastTaskExecutionTime = DateTime.Now;
        while (!this.cancellationToken.IsCancellationRequested)
        {
            await Task.Delay(this.taskTimeInterval);

            Console.WriteLine(
                $"\nNew task's execution\n" +
                $"Time since last execution : {(DateTime.Now - lastTaskExecutionTime).TotalSeconds} s");
            lastTaskExecutionTime = DateTime.Now;

            await this.CallDatabase();
        }
    }
}
