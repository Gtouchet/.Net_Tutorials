using BackgroundTasks;

/* 
 * Change to false to use the PeriodicTimer version
 * You should always use the PeriodicTimer version since it is more reliable than the Legacy version
 */
bool legacy = true;


BackgroundTask task = legacy ?
    new LegacyBackgroundTask(TimeSpan.FromMilliseconds(2_000)) :
    new PeriodicTimerBackgroundTask(TimeSpan.FromMilliseconds(2_000));


Console.WriteLine($"{(legacy ? "Legacy" : "Periodic")} task execution, push Enter to stop\n");
task.Start();
Console.ReadLine();
await task.Stop();
