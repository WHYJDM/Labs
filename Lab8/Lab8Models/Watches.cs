using System;

namespace Lab8Models;

/// <summary>
/// Represents a watches entity.
/// </summary>
public abstract class Watches
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
    /// Gets or sets the manufacturer ID associated with the watch.
    /// </summary>
    public int ManufacturerId { get; set; }

    /// <summary>
    /// Gets or sets the manufacturer associated with the watch.
    /// </summary>
    public virtual Manufacturer Manufacturer { get; set; }

    /// <summary>
    /// Prints the watch's details to the console.
    /// </summary>
    public virtual void PrintObject()
    {
        Console.WriteLine($"Id: {Id}, Model: {Model}, SerialNumber: {SerialNumber}, ManufacturerId: {ManufacturerId}");
    }
}