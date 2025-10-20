using Lab2Library;

/// <summary>
/// Manages the creation and printing of watches.
/// </summary>
public class WatchManager
{
    // Constants
    private const int NumberOfWatches = 10;
    private const int NumberOfTypes = 3;

    /// <summary>
    /// Creates a list of watches with sample data.
    /// </summary>
    public List<Watches> CreateWatches()
    {
        var watches = new List<Watches>();
        for (var i = 1; i <= NumberOfWatches; i++)
        {
            watches.Add(Watches.Create(
                i,
                $"Model{i}",
                $"SN{i}",
                (WatchesType)((i - 1) % NumberOfTypes)));
        }
        return watches;
    }

    /// <summary>
    /// Prints a list of watches.
    /// </summary>
    public void PrintWatches(List<Watches> watches)
    {
        foreach (var watch in watches)
        {
            watch.PrintObject();
        }
    }
}