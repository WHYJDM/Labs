using System.Text.Json;

namespace Lab5App;

/// <summary>
/// Represents a watch with its properties.
/// </summary>
public class Watches
{
    public required int Id { get; set; }

    public required string Model { get; set; }

    public required string SerialNumber { get; set; }
    public required WatchesType Type { get; set; }

    /// <summary>
    /// Creates a new instance of the <see cref="Watches"/> class.
    /// </summary>
    /// <param name="id">The unique identifier for the watch.</param>
    /// <param name="model">The model name of the watch.</param>
    /// <param name="serialNumber">The serial number of the watch.</param>
    /// <param name="type">The type of the watch.</param>
    /// <returns>A new <see cref="Watches"/> instance.</returns>
    public static Watches Create(int id, string model, string serialNumber, WatchesType type)
    {
        return new Watches { Id = id, Model = model, SerialNumber = serialNumber, Type = type };
    }

    /// <summary>
    /// Prints the watch details to the console.
    /// </summary>
    public void PrintObject()
    {
        Console.WriteLine($"Id: {Id}, Model: {Model}, SerialNumber: {SerialNumber}, Type: {Type}");
    }

    /// <summary>
    /// Serializes the watch to a JSON string.
    /// </summary>
    /// <returns>A JSON string representation of the watch.</returns>
    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }

    /// <summary>
    /// Deserializes a JSON string to a <see cref="Watches"/> instance.
    /// </summary>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <returns>A <see cref="Watches"/> instance.</returns>
    public static Watches FromJson(string json)
    {
        return JsonSerializer.Deserialize<Watches>(json)!;
    }
}