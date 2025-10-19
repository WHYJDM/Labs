// это чисто дл€ создани€ 10 экземпл€ров часов, а еще выводит их


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
    public List<Watches> CreateWatches()                          // метод который просто создает список часов 
    {
        var watches = new List<Watches>();
        for (var i = 1; i <= NumberOfWatches; i++)
        {
            watches.Add(Watches.Create(                           // добавл€ет новый объект Watches в список. дальше вызывает create, чтобы сделать новые часы
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