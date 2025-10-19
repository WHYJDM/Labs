using System;

namespace Lab8Models;

/// <summary>
/// Represents tower watches.
/// </summary>
public class TowerWatches : Watches
{
    /// <summary>
    /// Gets or sets the height of the tower.
    /// </summary>
    public double Height { get; set; }

    /// <summary>
    /// Gets or sets the location of the tower.
    /// </summary>
    public string Location { get; set; }

    /// <summary>
    /// Creates a new instance of the <see cref="TowerWatches"/> class.
    /// </summary>
    /// <param name="model">The model of the watch.</param>
    /// <param name="serialNumber">The serial number of the watch.</param>
    /// <param name="manufacturerId">The manufacturer ID.</param>
    /// <param name="height">The height of the tower.</param>
    /// <param name="location">The location of the tower.</param>
    /// <returns>A new <see cref="TowerWatches"/> instance.</returns>
    public static TowerWatches Create(string model, string serialNumber, int manufacturerId, double height, string location)
    {
        return new TowerWatches
        {
            Model = model,
            SerialNumber = serialNumber,
            ManufacturerId = manufacturerId,
            Height = height,
            Location = location
        };
    }

    /// <summary>
    /// Prints the tower watch's details to the console.
    /// </summary>
    public override void PrintObject()
    {
        base.PrintObject();
        Console.WriteLine($"Height: {Height}, Location: {Location}");
    }
}