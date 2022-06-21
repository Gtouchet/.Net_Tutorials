namespace BackgroundTasks;

/// <summary>
/// Effectue une tâche régulière <br/><br/>
/// La version Legacy ne respecte pas le temps entre 2 exécutiuons de la tâche, car un appel à une opération asynchrone va la décaler <br/>
/// La version utilisant un PeriodicTimer respecte ce temps entre les exécutions et n'est pas impactée par cet appel à l'opération asynchrone
/// </summary>
internal abstract class BackgroundTask
{
    /// <summary>
    /// Démarre la tâche répétée
    /// </summary>
    public abstract void Start();

    /// <summary>
    /// Boucle en effectuant une opération asynchrone <br/>
    /// Calcule et affiche le temps entre deux exécutions de tâche
    /// </summary>
    /// <returns></returns>
    protected abstract Task StartAsyncTask();

    /// <summary>
    /// Simule un appel asynchrone à une base de données
    /// </summary>
    /// <returns></returns>
    protected abstract Task CallDatabase();

    /// <summary>
    /// Arrête proprement la tâche
    /// </summary>
    /// <returns></returns>
    public abstract Task Stop();
}
