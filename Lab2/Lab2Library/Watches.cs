using System.Xml.Serialization;

namespace Lab2Library;

/// <summary>
/// Represents the type of watches.
/// </summary>
public enum WatchesType
{
    Electronic,
    Mechanic,
    Tower
}

/// <summary>
/// Represents a watch with its properties.
/// </summary>
public class Watches
{
    private int Id { get; set; }

    [XmlAttribute]
    public string? Model { get; set; }

    public string? SerialNumber { get; set; }
    public WatchesType Type { get; set; }

    /// <summary>
    /// Creates a new instance of Watches.
    /// </summary>
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
}