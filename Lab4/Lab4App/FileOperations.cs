using System.Text.Json;
using System.Threading;

namespace Lab4App;

/// <summary>
/// Handles file operations for reading and writing watches data.
/// </summary>
public class FileOperations
{
    private const int MaxConcurrency = 1;
    private static readonly SemaphoreSlim fileSemaphore = new(MaxConcurrency, MaxConcurrency);

    /// <summary>
    /// Writes a collection of watches to a specified file in JSON format.
    /// </summary>
    /// <param name="items">The watches to write.</param>
    /// <param name="fileName">The name of the file to write to.</param>
    public void WriteToFile(IEnumerable<Watches> items, string fileName)
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

    /// <summary>
    /// Reads watches data from a specified file and deserializes them from JSON.
    /// </summary>
    /// <param name="fileName">The name of the file to read from.</param>
    /// <returns>A list of Watches objects.</returns>
    public List<Watches> ReadFromFile(string fileName)
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

    /// <summary>
    /// Asynchronously reads from a file and prints each watch's JSON representation concurrently.
    /// </summary>
    /// <param name="fileName">The name of the file to read from.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task PrintFromFileAsync(string fileName)
    {
        await fileSemaphore.WaitAsync();
        try
        {
            var tasks = new List<Task>();
            using var reader = new StreamReader(fileName);
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