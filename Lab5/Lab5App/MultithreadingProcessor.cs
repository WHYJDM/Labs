using System.Collections.Concurrent;
using System.Text.Json;

namespace Lab5App;

/// <summary>
/// Processes multithreading tasks for watches data.
/// </summary>
public class MultithreadingProcessor
{
    private const string File1 = "file1.json";
    private const string File2 = "file2.json";
    private const string File3 = "file3.json";
    private const string File4 = "file4.json";
    private const string File5 = "file5.json";
    private const string CombinedFile = "combined.json";

    private readonly List<Watches> watches;
    private readonly ConcurrentDictionary<string, List<Watches>> dataDictionary = new();
    private readonly object dataLock = new();
    private readonly object progressLock = new();
    private int totalRecords;
    private int processedRecords;

    /// <summary>
    /// Initializes a new instance of the <see cref="MultithreadingProcessor"/> class.
    /// </summary>
    public MultithreadingProcessor()
    {
        watches = GenerateWatches();
    }

    private List<Watches> GenerateWatches()
    {
        var list = new List<Watches>();
        for (int i = 1; i <= 50; i++)
        {
            var type = (WatchesType)((i - 1) % 3);
            list.Add(Watches.Create(i, $"Model{i}", $"SN{i}", type));
        }
        return list;
    }


    /// <summary>
    /// Writes watches data to five files asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous write operation.</returns>
    public async Task WriteFilesAsync()
    {
        var tasks = new List<Task>();
        for (int i = 0; i < 5; i++)
        {
            var fileIndex = i;
            var fileName = fileIndex switch
            {
                0 => File1,
                1 => File2,
                2 => File3,
                3 => File4,
                4 => File5,
                _ => throw new InvalidOperationException()
            };
            tasks.Add(WriteToFileAsync(watches.Skip(fileIndex * 10).Take(10), fileName));
        }
        await Task.WhenAll(tasks);
    }

    private async Task WriteToFileAsync(IEnumerable<Watches> items, string fileName)
    {
        using var writer = new StreamWriter(fileName);
        foreach (var watch in items)
        {
            await writer.WriteLineAsync(watch.ToJson());
        }
    }

    /// <summary>
    /// Reads from five files asynchronously, combines into a combined file, and prints data.
    /// </summary>
    /// <returns>A task that represents the asynchronous read and combine operation.</returns>
    public async Task ReadFilesAsync()
    {
        var files = new[] { File1, File2, File3, File4, File5 };
        var readTasks = new List<Task<List<Watches>>>();
        foreach (var file in files)
        {
            readTasks.Add(ReadFromFile(file));
        }
        var results = await Task.WhenAll(readTasks);

        var allRecords = results.SelectMany(r => r).ToList();
        WriteCombinedToFile(allRecords, CombinedFile);

        lock (dataLock)
        {
            foreach (var kvp in dataDictionary)
            {
                Console.WriteLine($"Key: {kvp.Key}");
                foreach (var watch in kvp.Value)
                {
                    watch.PrintObject();
                }
            }
        }
    }

    private async Task<List<Watches>> ReadFromFile(string fileName)
    {
        var list = dataDictionary.GetOrAdd(fileName, _ => new List<Watches>());
        var localList = new List<Watches>();
        using var reader = new StreamReader(fileName);
        string? line;
        while ((line = await reader.ReadLineAsync()) is not null)
        {
            var watch = Watches.FromJson(line);
            localList.Add(watch);
            await Task.Delay(100);
            UpdateProgress();
        }
        lock (dataLock)
        {
            list.AddRange(localList);
        }
        return localList;
    }

    private void WriteCombinedToFile(IEnumerable<Watches> items, string fileName)
    {
        using var writer = new StreamWriter(fileName);
        foreach (var watch in items)
        {
            writer.WriteLine(watch.ToJson());
        }
    }

    /// <summary>
    /// Starts the sorting process for the data dictionary.
    /// </summary>
    /// <returns>A task that represents the asynchronous sorting process.</returns>
    public async Task StartSortingProcess()
    {
        var sorter = new Sorter(dataDictionary, dataLock);
        sorter.StartSorting();
        await Task.Delay(5000);
        await sorter.StopSortingAsync();
    }

    private void UpdateProgress()
    {
        lock (progressLock)
        {
            processedRecords++;
            DrawProgressBar(processedRecords, totalRecords);
        }
    }

    private void DrawProgressBar(int current, int total)
    {
        if (total == 0) return;
        var percentage = (double)current / total;
        var barLength = 50;
        var filled = (int)(percentage * barLength);
        var bar = new string('█', filled) + new string('░', barLength - filled);
        Console.Write($"\rProgress: [{bar}] {current}/{total} ({percentage:P0})");
        if (current == total) Console.WriteLine();
    }

    /// <summary>
    /// Sets the total number of records for progress tracking.
    /// </summary>
    /// <param name="total">The total number of records.</param>
    public void SetTotalRecords(int total)
    {
        totalRecords = total;
    }
}