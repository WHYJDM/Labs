namespace Lab4App;

/// <summary>
/// Generates watches data.
/// </summary>
public class WatchesDataGenerator
{
    private const int TotalWatches = 20;
    private const int WatchesTypesCount = 3;

    /// <summary>
    /// Generates a list of watches with predefined data.
    /// </summary>
    /// <returns>A list of Watches objects.</returns>
    public List<Watches> GenerateWatches()
    {
        var list = new List<Watches>();
        for (int i = 1; i <= TotalWatches; i++)
        {
            var type = (WatchesType)((i - 1) % WatchesTypesCount);
            list.Add(Watches.Create(i, $"Model{i}", $"SN{i}", type));
        }
        return list;
    }
}