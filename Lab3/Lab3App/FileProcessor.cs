using System.Diagnostics;

namespace Lab3App;

public class FileProcessor
{
    private readonly object _lock = new();

    public void Task1(List<Watches> watches)
    {
        var first10 = watches.Take(10).ToList();
        var second10 = watches.Skip(10).ToList();

        Thread thread1 = new(() => WriteToFile(first10, "file1.json"));
        Thread thread2 = new(() => WriteToFile(second10, "file2.json"));

        thread1.Start();
        thread2.Start();

        thread1.Join();
        thread2.Join();
    }

    private void WriteToFile(List<Watches> watches, string fileName)
    {
        lock (_lock)
        {
            using StreamWriter writer = new(fileName);
            foreach (var watch in watches)
            {
                writer.WriteLine(watch.ToJson());
            }
        }
    }

    public void Task2()
    {
        List<string> lines1 = new();
        List<string> lines2 = new();

        Thread thread1 = new(() =>
        {
            lock (_lock)
            {
                lines1 = File.ReadAllLines("file1.json").ToList();
            }
        });
        Thread thread2 = new(() =>
        {
            lock (_lock)
            {
                lines2 = File.ReadAllLines("file2.json").ToList();
            }
        });

        thread1.Start();
        thread2.Start();

        thread1.Join();
        thread2.Join();

        using StreamWriter writer = new("file3.json");
        for (int i = 0; i < 10; i++)
        {
            writer.WriteLine(lines1[i]);
            writer.WriteLine(lines2[i]);
        }
    }

    public void Task3_1()
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        var lines = File.ReadAllLines("file3.json");
        foreach (var line in lines)
        {
            var watch = Watches.FromJson(line);
            watch.PrintObject();
        }
        stopwatch.Stop();
        Console.WriteLine($"Time: {stopwatch.ElapsedMilliseconds} ms");
    }

    public void Task3_2()
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        var lines = File.ReadAllLines("file3.json");
        var half = lines.Length / 2;
        var firstHalf = lines.Take(half).ToList();
        var secondHalf = lines.Skip(half).ToList();

        Thread thread1 = new(() =>
        {
            foreach (var line in firstHalf)
            {
                var watch = Watches.FromJson(line);
                watch.PrintObject();
            }
        });
        Thread thread2 = new(() =>
        {
            foreach (var line in secondHalf)
            {
                var watch = Watches.FromJson(line);
                watch.PrintObject();
            }
        });

        thread1.Start();
        thread2.Start();

        thread1.Join();
        thread2.Join();

        stopwatch.Stop();
        Console.WriteLine($"Time: {stopwatch.ElapsedMilliseconds} ms");
    }

    public void Task3_3()
    {
        Semaphore semaphore = new(5, 5);
        List<Thread> threads = new();
        Stopwatch stopwatch = Stopwatch.StartNew();

        for (int i = 0; i < 10; i++)
        {
            Thread thread = new(() =>
            {
                semaphore.WaitOne();
                try
                {
                    var lines = File.ReadAllLines("file3.json");
                    foreach (var line in lines)
                    {
                        var watch = Watches.FromJson(line);
                        watch.PrintObject();
                    }
                }
                finally
                {
                    semaphore.Release();
                }
            });
            threads.Add(thread);
            thread.Start();
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }

        stopwatch.Stop();
        Console.WriteLine($"Time: {stopwatch.ElapsedMilliseconds} ms");
    }
}