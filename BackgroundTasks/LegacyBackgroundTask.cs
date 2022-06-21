namespace BackgroundTasks;

internal class LegacyBackgroundTask : BackgroundTask
{
    private Task timerTask;
    private readonly CancellationTokenSource cancellationToken;
    private readonly TimeSpan taskTimeInterval;
    private readonly Random random;

    public LegacyBackgroundTask(TimeSpan taskTimeInterval)
    {
        this.taskTimeInterval = taskTimeInterval;
        this.cancellationToken = new CancellationTokenSource();
        this.random = new Random();
    }

    public override void Start()
    {
        Console.WriteLine("Tâche démarée");
        this.timerTask = this.StartAsyncTask();
    }

    protected override async Task StartAsyncTask()
    {
        DateTime lastTaskExecutionTime = DateTime.Now;
        while (!this.cancellationToken.IsCancellationRequested)
        {
            await Task.Delay(this.taskTimeInterval);

            Console.WriteLine(
                $"\nNouvelle exécution de tâche\n" +
                $"Temps depuis la dernière exécution : {(DateTime.Now - lastTaskExecutionTime).TotalSeconds} s");
            lastTaskExecutionTime = DateTime.Now;

            await this.CallDatabase();
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
