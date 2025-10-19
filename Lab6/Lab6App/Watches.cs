using System;
using System.Text.Json;

namespace Lab6App;

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
/// Represents a watches entity.
/// </summary>
public class Watches
{
    /// <summary>
    /// Gets or sets the unique identifier for the watches.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the model of the watches.
    /// </summary>
    public required string Model { get; set; }

    /// <summary>
    /// Gets or sets the serial number of the watches.
    /// </summary>
    public required string SerialNumber { get; set; }

    /// <summary>
    /// Gets or sets the type of the watches.
    /// </summary>
    public WatchesType Type { get; set; }

    /// <summary>
    /// Gets or sets the manufacturer identifier.
    /// </summary>
    public int ManufacturerId { get; set; }

    /// <summary>
    /// Creates a new instance of the <see cref="Watches"/> class.
    /// </summary>
    /// <param name="model">The model of the watches.</param>
    /// <param name="serialNumber">The serial number of the watches.</param>
    /// <param name="type">The type of the watches.</param>
    /// <param name="manufacturerId">The manufacturer identifier.</param>
    /// <returns>A new <see cref="Watches"/> instance.</returns>
    public static Watches Create(string model, string serialNumber, WatchesType type, int manufacturerId)
    {
        return new Watches { Model = model, SerialNumber = serialNumber, Type = type, ManufacturerId = manufacturerId };
    }

    /// <summary>
    /// Prints the details of the watches to the console.
    /// </summary>
    public void PrintObject()
    {
        Console.WriteLine($"Id: {Id}, Model: {Model}, SerialNumber: {SerialNumber}, Type: {Type}, ManufacturerId: {ManufacturerId}");
    }

    /// <summary>
    /// Serializes the watches object to a JSON string.
    /// </summary>
    /// <returns>A JSON string representation of the watches.</returns>
    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }

    /// <summary>
    /// Deserializes a JSON string to a <see cref="Watches"/> object.
    /// </summary>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <returns>A <see cref="Watches"/> object.</returns>
    public static Watches FromJson(string json)
    {
        return JsonSerializer.Deserialize<Watches>(json)!;
    }
}