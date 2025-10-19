namespace Lab3App;

/// <summary>
/// Processes and prints watches sequentially from the file.
/// </summary>
public class SequentialProcessor : WatchProcessor // ������������ | ������ ����������� ProcessAndPrint
{
    /// <summary>
    /// Reads all lines from file and processes them one by one.
    /// </summary>
    public override void ProcessAndPrint() // override �������������� ����� 
    {
        var lines = File.ReadAllLines(Constants.File3); // ������ ���� ���� ������ ������
        foreach (var line in lines)
        {
            var watch = Watches.FromJson(line); // json ������� � ������ Watches
            watch.PrintObject();
        }
    }
}