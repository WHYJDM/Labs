using System.Text.Json;

namespace Lab4App;

public class FileTaskProcessor
{
    private static readonly SemaphoreSlim fileSemaphore = new(1, 1);
    private readonly List<Watches> watches;

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

    public void Task1()
    {
        var task1 = Task.Run(() => WriteToFile(watches.Take(10), "file1.json"));
        var task2 = Task.Run(() => WriteToFile(watches.Skip(10), "file2.json"));
        Task.WaitAll(task1, task2);
    }

    private void WriteToFile(IEnumerable<Watches> items, string fileName)
    {
        fileSemaphore.Wait();
        try
        {
            using var writer = new StreamWriter(fileName);
            foreach (var watch in items)
            {
                writer.WriteLine(watch.ToJson());
            }
        }
        finally
        {
            fileSemaphore.Release();
        }
    }

    public void Task2()
    {
        var readTask1 = Task.Run(() => ReadFromFile("file1.json"));
        var readTask2 = Task.Run(() => ReadFromFile("file2.json"));
        Task.WaitAll(readTask1, readTask2);
        var data1 = readTask1.Result;
        var data2 = readTask2.Result;
        WriteCombinedToFile(data1.Concat(data2), "file3.json");
    }

    private List<Watches> ReadFromFile(string fileName)
    {
        fileSemaphore.Wait();
        try
        {
            var list = new List<Watches>();
            using var reader = new StreamReader(fileName);
            string? line;
            while ((line = reader.ReadLine()) is not null)
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

    private void WriteCombinedToFile(IEnumerable<Watches> items, string fileName)
    {
        fileSemaphore.Wait();
        try
        {
            using var writer = new StreamWriter(fileName);
            foreach (var watch in items)
            {
                writer.WriteLine(watch.ToJson());
            }
        }
        finally
        {
            fileSemaphore.Release();
        }
    }

    public async Task Task3()
    {
        await fileSemaphore.WaitAsync();
        try
        {
            var tasks = new List<Task>();
            using var reader = new StreamReader("file3.json");
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