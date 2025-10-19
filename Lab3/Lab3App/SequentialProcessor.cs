namespace Lab3App;

/// <summary>
/// Processes and prints watches sequentially from the file.
/// </summary>
public class SequentialProcessor : WatchProcessor // наследование | обязан реализовать ProcessAndPrint
{
    /// <summary>
    /// Reads all lines from file and processes them one by one.
    /// </summary>
    public override void ProcessAndPrint() // override переопределяет метод 
    {
        var lines = File.ReadAllLines(Constants.File3); // читает весь файл отдает стринг
        foreach (var line in lines)
        {
            var watch = Watches.FromJson(line); // json обратно в обьект Watches
            watch.PrintObject();
        }
    }
}