using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

/// <summary>
/// Constants class to hold all constant values used in the application.
/// </summary>
public static class Constants
{
    /// <summary>
    /// The total number of watches to generate.
    /// </summary>
    public const int WatchCount = 20;

    /// <summary>
    /// The size for splitting watches (not used in current implementation, but extracted as per requirements).
    /// </summary>
    public const int SplitSize = 10;

    /// <summary>
    /// The number of different types of watches.
    /// </summary>
    public const int TypeCount = 3;

    /// <summary>
    /// Array of file names for output JSON files.
    /// </summary>
    public static readonly string[] Files = { "file1.json", "file2.json", "file3.json" };
}

/// <summary>
/// Represents a watch with a type and name.
/// </summary>
public class Watch
{
    /// <summary>
    /// The type of the watch.
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// The name of the watch.
    /// </summary>
    public string Name { get; set; }
}

/// <summary>
/// Class responsible for generating a list of watches.
/// </summary>
public class WatchGenerator
{
    /// <summary>
    /// Generates a list of watches based on the constants.
    /// </summary>
    /// <returns>A list of Watch objects.</returns>
    public List<Watch> GenerateWatches()
    {
        var watches = new List<Watch>();
        for (int i = 0; i < Constants.WatchCount; i++)
        {
            watches.Add(new Watch { Type = i % Constants.TypeCount, Name = "Watch" + i });
        }
        return watches;
    }
}

/// <summary>
/// Class responsible for processing the list of watches and writing them to JSON files.
/// </summary>
public class FileProcessor
{
    /// <summary>
    /// Processes the watches by grouping them by type and writing each group to a JSON file.
    /// </summary>
    /// <param name="watches">The list of watches to process.</param>
    public void ProcessAndWrite(List<Watch> watches)
    {
        var groups = watches.GroupBy(w => w.Type);
        int index = 0;
        foreach (var group in groups)
        {
            var json = JsonConvert.SerializeObject(group.ToList());
            File.WriteAllText(Constants.Files[index], json);
            index++;
        }
    }
}

/// <summary>
/// Main class that orchestrates the watch generation and file processing.
/// </summary>
public class FileTaskProcessor
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create instances of the classes
        var generator = new WatchGenerator();
        var processor = new FileProcessor();

        // Generate the list of watches
        var watches = generator.GenerateWatches();

        // Process and write the watches to files
        processor.ProcessAndWrite(watches);
    }
}