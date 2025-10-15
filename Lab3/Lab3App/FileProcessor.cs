using System.Diagnostics;

namespace Lab3App;

/// <summary>
/// Main processor class that orchestrates file operations and watch processing tasks.
/// </summary>
public class FileProcessor
{
    /// <summary>
    /// Executes Task 1: Splits watches and writes to files.
    /// </summary>
    /// <param name="watches">The list of watches.</param>
    public void Task1(List<Watches> watches)
    {
        FileSplitter splitter = new();
        splitter.SplitAndWrite(watches);
    }

    /// <summary>
    /// Executes Task 2: Merges two files into one.
    /// </summary>
    public void Task2()
    {
        FileMerger merger = new();
        merger.MergeFiles();
    }

    /// <summary>
    /// Executes Task 3.1: Processes watches sequentially and measures time.
    /// </summary>
    public void Task3_1()
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        WatchProcessor processor = new SequentialProcessor();
        processor.ProcessAndPrint();
        stopwatch.Stop();
        Console.WriteLine($"Time: {stopwatch.ElapsedMilliseconds} ms");
    }

    /// <summary>
    /// Executes Task 3.2: Processes watches in parallel and measures time.
    /// </summary>
    public void Task3_2()
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        WatchProcessor processor = new ParallelProcessor();
        processor.ProcessAndPrint();
        stopwatch.Stop();
        Console.WriteLine($"Time: {stopwatch.ElapsedMilliseconds} ms");
    }

    /// <summary>
    /// Executes Task 3.3: Processes watches with semaphore-controlled threads and measures time.
    /// </summary>
    public void Task3_3()
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        WatchProcessor processor = new SemaphoreProcessor();
        processor.ProcessAndPrint();
        stopwatch.Stop();
        Console.WriteLine($"Time: {stopwatch.ElapsedMilliseconds} ms");
    }
}