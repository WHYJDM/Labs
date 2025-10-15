namespace Lab3App;

/// <summary>
/// Processes and prints watches sequentially from the file.
/// </summary>
public class SequentialProcessor : WatchProcessor
{
    /// <summary>
    /// Reads all lines from file and processes them one by one.
    /// </summary>
    public override void ProcessAndPrint()
    {
        var lines = File.ReadAllLines(Constants.File3);
        foreach (var line in lines)
        {
            var watch = Watches.FromJson(line);
            watch.PrintObject();
        }
    }
}