namespace Lab3App;

/// <summary>
/// Class responsible for merging two files into one by reading with threads.
/// </summary>
public class FileMerger
{
    private readonly object _lock = new();

    /// <summary>
    /// Reads lines from two files concurrently and merges them alternately into a third file.
    /// </summary>
    public void MergeFiles()
    {
        List<string> lines1 = new();
        List<string> lines2 = new();

        Thread thread1 = new(() =>
        {
            lock (_lock)
            {
                lines1 = File.ReadAllLines(Constants.File1).ToList();
            }
        });
        Thread thread2 = new(() =>
        {
            lock (_lock)
            {
                lines2 = File.ReadAllLines(Constants.File2).ToList();
            }
        });

        thread1.Start();
        thread2.Start();

        thread1.Join();
        thread2.Join();

        using StreamWriter writer = new(Constants.File3);
        for (int i = 0; i < Constants.HalfCount; i++)
        {
            writer.WriteLine(lines1[i]);
            writer.WriteLine(lines2[i]);
        }
    }
}