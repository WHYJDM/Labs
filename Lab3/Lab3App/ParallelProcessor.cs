namespace Lab3App;

/// <summary>
/// Processes and prints watches in parallel using two threads.
/// </summary>
public class ParallelProcessor : WatchProcessor
{
    /// <summary>
    /// Splits the file lines into two halves and processes each in a separate thread.
    /// </summary>
    public override void ProcessAndPrint()
    {
        var lines = File.ReadAllLines(Constants.File3);
        var half = lines.Length / 2;
        var firstHalf = lines.Take(half).ToList();
        var secondHalf = lines.Skip(half).ToList();

        Thread thread1 = new(() =>
        {
            foreach (var line in firstHalf)
            {
                var watch = Watches.FromJson(line);
                watch.PrintObject();
            }
        });
        Thread thread2 = new(() =>
        {
            foreach (var line in secondHalf)
            {
                var watch = Watches.FromJson(line);
                watch.PrintObject();
            }
        });

        thread1.Start();
        thread2.Start();

        thread1.Join();
        thread2.Join();
    }
}