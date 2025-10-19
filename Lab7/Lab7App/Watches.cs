using System;
using System.Text.Json;

namespace Lab7App;

/// <summary>
/// Represents a watches entity.
/// </summary>
public class Watches
{
    /// <summary>
    /// Gets or sets the unique identifier for the watch.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the model of the watch.
    /// </summary>
    public string Model { get; set; }

    /// <summary>
    /// Gets or sets the serial number of the watch.
    /// </summary>
    public string SerialNumber { get; set; }

    /// <summary>
    /// Gets or sets the type of the watch.
    /// </summary>
    public WatchesType Type { get; set; }

    /// <summary>
    /// Gets or sets the manufacturer ID associated with the watch.
    /// </summary>
    public int ManufacturerId { get; set; }

    /// <summary>
    /// Gets or sets the manufacturer associated with the watch.
    /// </summary>
    public virtual Manufacturer Manufacturer { get; set; }

    /// <summary>
    /// Creates a new instance of the <see cref="Watches"/> class.
    /// </summary>
    /// <param name="model">The model of the watch.</param>
    /// <param name="serialNumber">The serial number of the watch.</param>
    /// <param name="type">The type of the watch.</param>
    /// <param name="manufacturerId">The manufacturer ID.</param>
    /// <returns>A new <see cref="Watches"/> instance.</returns>
    public static Watches Create(string model, string serialNumber, WatchesType type, int manufacturerId)
    {
        return new Watches { Model = model, SerialNumber = serialNumber, Type = type, ManufacturerId = manufacturerId };
    }

    /// <summary>
    /// Prints the watch's details to the console.
    /// </summary>
    public void PrintObject()
    {
        Console.WriteLine($"Id: {Id}, Model: {Model}, SerialNumber: {SerialNumber}, Type: {Type}, ManufacturerId: {ManufacturerId}");
    }
}