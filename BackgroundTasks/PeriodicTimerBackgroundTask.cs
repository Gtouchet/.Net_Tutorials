namespace BackgroundTasks;

internal sealed class PeriodicTimerBackgroundTask : BackgroundTask
{
    private Task timerTask;
    private readonly PeriodicTimer timer;
    private readonly CancellationTokenSource cancellationToken;
    private readonly TimeSpan taskTimeInterval;
    private readonly Random random;

    public PeriodicTimerBackgroundTask(TimeSpan taskTimeInterval)
    {
        this.timer = new PeriodicTimer(taskTimeInterval);
        this.cancellationToken = new CancellationTokenSource();
        this.taskTimeInterval = taskTimeInterval;
        this.random = new Random();
    }

    public override void Start()
    {
        Console.WriteLine("Tâche démarée");
        this.timerTask = this.StartAsyncTask();
    }

    protected override async Task StartAsyncTask()
    {
        try
        {
            DateTime lastTaskExecutionTime = DateTime.Now;
            while (await this.timer.WaitForNextTickAsync(this.cancellationToken.Token))
            {
                Console.WriteLine(
                    $"\nNouvelle exécution de tâche\n" +
                    $"Temps depuis la dernière exécution : {(DateTime.Now - lastTaskExecutionTime).TotalSeconds} s");
                lastTaskExecutionTime = DateTime.Now;

                await this.CallDatabase();
            }
        }
        catch (OperationCanceledException)
        {

        }
    }

    protected override async Task CallDatabase()
    {
        int databaseTaskTimeMs = this.random.Next(0, (int)this.taskTimeInterval.TotalMilliseconds);
        await Task.Delay(databaseTaskTimeMs);
        Console.WriteLine($"Opération sur la base de données terminée après {databaseTaskTimeMs} ms");
    }

    public override async Task Stop()
    {
        if (this.timerTask == null)
        {
            return;
        }

        this.cancellationToken.Cancel();
        await this.timerTask;
        this.cancellationToken.Dispose();

        Console.WriteLine("Tâche annulée");
    }
}
