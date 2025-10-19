using System.Collections.Concurrent;

namespace Lab5App;

/// <summary>
/// Sorts the watches data in a concurrent dictionary.
/// </summary>
public class Sorter
{
    private readonly ConcurrentDictionary<string, List<Watches>> dataDictionary;
    private readonly object dataLock;
    private readonly CancellationTokenSource cts = new();
    private Task? sortingTask;

    /// <summary>
    /// Initializes a new instance of the <see cref="Sorter"/> class.
    /// </summary>
    /// <param name="dictionary">The concurrent dictionary to sort.</param>
    /// <param name="dataLock">The lock object for thread safety.</param>
    public Sorter(ConcurrentDictionary<string, List<Watches>> dictionary, object dataLock)
    {
        this.dataDictionary = dictionary;
        this.dataLock = dataLock;
    }

    /// <summary>
    /// Starts the sorting thread.
    /// </summary>
    public void StartSorting()
    {
        sortingTask = SortLoopAsync();
    }

    /// <summary>
    /// Stops the sorting thread.
    /// </summary>
    public async Task StopSortingAsync()
    {
        cts.Cancel();
        if (sortingTask != null)
        {
            try
            {
                await sortingTask;
            }
            catch (TaskCanceledException)
            {
                // Expected when canceling the task
            }
        }
    }

    private async Task SortLoopAsync()
    {
        while (!cts.Token.IsCancellationRequested)
        {
            lock (dataLock)
            {
                foreach (var kvp in dataDictionary)
                {
                    var sortedList = kvp.Value.OrderBy(w => w.Id).ToList();
                    kvp.Value.Clear();
                    kvp.Value.AddRange(sortedList);
                }
            }
            await Task.Delay(1000, cts.Token);
        }
    }
}