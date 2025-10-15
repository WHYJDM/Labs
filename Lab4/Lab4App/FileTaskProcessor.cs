using System.Text.Json;

namespace Lab4App;

/// <summary>
/// Processes file tasks for watches data, including writing, reading, and combining files asynchronously.
/// </summary>
public class FileTaskProcessor
{
    private const int WatchesPerFile = 10;
    private const string File1Name = "file1.json";
    private const string File2Name = "file2.json";
    private const string File3Name = "file3.json";

    private readonly List<Watches> watches;
    private readonly FileOperations fileOperations;

    /// <summary>
    /// Initializes a new instance of the FileTaskProcessor class.
    /// </summary>
    public FileTaskProcessor()
    {
        var generator = new WatchesDataGenerator();
        watches = generator.GenerateWatches();
        fileOperations = new FileOperations();
    }

    /// <summary>
    /// Executes Task 1: Writes the first half of watches to file1.json and the second half to file2.json concurrently.
    /// </summary>
    public void Task1()
    {
        var task1 = Task.Run(() => fileOperations.WriteToFile(watches.Take(WatchesPerFile), File1Name));
        var task2 = Task.Run(() => fileOperations.WriteToFile(watches.Skip(WatchesPerFile), File2Name));
        Task.WaitAll(task1, task2);
    }

    /// <summary>
    /// Executes Task 2: Reads data from file1.json and file2.json concurrently, then combines and writes to file3.json.
    /// </summary>
    public void Task2()
    {
        var readTask1 = Task.Run(() => fileOperations.ReadFromFile(File1Name));
        var readTask2 = Task.Run(() => fileOperations.ReadFromFile(File2Name));
        Task.WaitAll(readTask1, readTask2);
        var data1 = readTask1.Result;
        var data2 = readTask2.Result;
        fileOperations.WriteToFile(data1.Concat(data2), File3Name);
    }

    /// <summary>
    /// Executes Task 3: Asynchronously reads from file3.json and prints each watch's JSON representation concurrently.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task Task3()
    {
        await fileOperations.PrintFromFileAsync(File3Name);
    }
}