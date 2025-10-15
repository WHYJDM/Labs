namespace Lab3App;

/// <summary>
/// Processes and prints watches using multiple threads controlled by a semaphore.
/// </summary>
public class SemaphoreProcessor : WatchProcessor
{
    /// <summary>
    /// Creates multiple threads, each processing the entire file, limited by semaphore.
    /// </summary>
    public override void ProcessAndPrint()
    {
        Semaphore semaphore = new(Constants.MaxSemaphoreCount, Constants.MaxSemaphoreCount);
        List<Thread> threads = new();

        for (int i = 0; i < Constants.NumberOfThreadsForTask3_3; i++)
        {
            Thread thread = new(() =>
            {
                semaphore.WaitOne();
                try
                {
                    var lines = File.ReadAllLines(Constants.File3);
                    foreach (var line in lines)
                    {
                        var watch = Watches.FromJson(line);
                        watch.PrintObject();
                    }
                }
                finally
                {
                    semaphore.Release();
                }
            });
            threads.Add(thread);
            thread.Start();
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }
    }
}