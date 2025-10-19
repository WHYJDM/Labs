namespace Lab3App;

/// <summary>
/// Class responsible for splitting a list of watches and writing to files using threads.
/// </summary>
public class FileSplitter
{
    private readonly object _lock = new(); // �������� ��� ������ 1 ����� ������ ��������� ��� �� ������������ ������� �������

    /// <summary>
    /// Splits the watches list into two halves and writes each to a separate file concurrently.
    /// </summary>
    /// <param name="watches">The list of watches to split and write.</param>
    public void SplitAndWrite(List<Watches> watches)
    {
        var first10 = watches.Take(Constants.HalfCount).ToList();
        var second10 = watches.Skip(Constants.HalfCount).ToList();

        Thread thread1 = new(() => WriteToFile(first10, Constants.File1));
        Thread thread2 = new(() => WriteToFile(second10, Constants.File2));

        thread1.Start(); // ������������ ����������
        thread2.Start();

        thread1.Join();
        thread2.Join();
    }

    /// <summary>
    /// Writes a list of watches to a file in JSON format with thread-safe locking.
    /// </summary>
    /// <param name="watches">The watches to write.</param>
    /// <param name="fileName">The target file name.</param>
    private void WriteToFile(List<Watches> watches, string fileName)
    {
        lock (_lock) // ������ �������� ������
        {
            using StreamWriter writer = new(fileName); //  StreamWriter ��������� ����� ��� ������ � ��������� ����| Using �����������, ��� ���� ����� ������ � ��������� ����� ����������
            foreach (var watch in watches)
            {
                writer.WriteLine(watch.ToJson()); // �� ��� ������ � json
            }
        }
    }
}