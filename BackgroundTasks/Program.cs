using BackgroundTasks;

// Changez en false pour tester la tâche avec PeriodicTimer
bool legacyTask = true;


BackgroundTask task = legacyTask ?
    new LegacyBackgroundTask(TimeSpan.FromMilliseconds(2_000)) :
    new PeriodicTimerBackgroundTask(TimeSpan.FromMilliseconds(2_000));


Console.WriteLine($"Exécution de tâche {(legacyTask ? "legacy" : "périodique")}, appuyez sur entrée pour arrêter la tâche\n");
task.Start();
Console.ReadLine();
await task.Stop();
