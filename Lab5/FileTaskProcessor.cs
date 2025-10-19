using System.Text.Json;

namespace Lab5App;

/// <summary>
/// Processes file tasks for watches data using concurrency and asynchrony.
/// </summary>
public class FileTaskProcessor
{
    private const string File1 = "file1.json";
    private const string File2 = "file2.json";
    private const string File3 = "file3.json";

    private static readonly SemaphoreSlim fileSemaphore = new(1, 1);
    private readonly List<Watches> watches;

    /// <summary>
    /// Initializes a new instance of the <see cref="FileTaskProcessor"/> class.
    /// </summary>
    public FileTaskProcessor()
    {
        watches = GenerateWatches();
    }

    private List<Watches> GenerateWatches()
    {
        var list = new List<Watches>();
        for (int i = 1; i <= 20; i++)
        {
            var type = (WatchesType)((i - 1) % 3);
            list.Add(Watches.Create(i, $"Model{i}", $"SN{i}", type));
        }
        return list;
    }

    /// <summary>
    /// Executes Task 1: Writes watches data to two files concurrently.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task Task1()
    {
        var task1 = Task.Run(() => WriteToFile(watches.Take(10), File1));
        var task2 = Task.Run(() => WriteToFile(watches.Skip(10), File2));
        await Task.WhenAll(task1, task2);
    }

    private async Task WriteToFile(IEnumerable<Watches> items, string fileName)
    {
        await fileSemaphore.WaitAsync();
        try
        {
            using var writer = new StreamWriter(fileName);
            foreach (var watch in items)
            {
                await writer.WriteLineAsync(watch.ToJson());
            }
        }
        finally
        {
            fileSemaphore.Release();
        }
    }

    /// <summary>
    /// Executes Task 2: Reads from two files concurrently and combines into a third file.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task Task2()
    {
        var readTask1 = Task.Run(() => ReadFromFile(File1));
        var readTask2 = Task.Run(() => ReadFromFile(File2));
        await Task.WhenAll(readTask1, readTask2);
        var data1 = readTask1.Result;
        var data2 = readTask2.Result;
        await WriteCombinedToFile(data1.Concat(data2), File3);
    }

    private async Task<List<Watches>> ReadFromFile(string fileName)
    {
        await fileSemaphore.WaitAsync();
        try
        {
            var list = new List<Watches>();
            using var reader = new StreamReader(fileName);
            string? line;
            while ((line = await reader.ReadLineAsync()) is not null)
            {
                list.Add(Watches.FromJson(line));
            }
            return list;
        }
        finally
        {
            fileSemaphore.Release();
        }
    }

    private async Task WriteCombinedToFile(IEnumerable<Watches> items, string fileName)
    {
        await fileSemaphore.WaitAsync();
        try
        {
            using var writer = new StreamWriter(fileName);
            foreach (var watch in items)
            {
                await writer.WriteLineAsync(watch.ToJson());
            }
        }
        finally
        {
            fileSemaphore.Release();
        }
    }

    /// <summary>
    /// Executes Task 3: Asynchronously reads from file3 and prints data.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task Task3()
    {
        await fileSemaphore.WaitAsync();
        try
        {
            var tasks = new List<Task>();
            using var reader = new StreamReader(File3);
            string? line;
            while ((line = await reader.ReadLineAsync()) is not null)
            {
                var watch = Watches.FromJson(line);
                tasks.Add(Task.Run(() => Console.WriteLine(watch.ToJson())));
            }
            await Task.WhenAll(tasks);
        }
        finally
        {
            fileSemaphore.Release();
        }
    }
}