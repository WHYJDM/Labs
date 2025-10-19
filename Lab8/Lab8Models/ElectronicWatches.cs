using System;

namespace Lab8Models;

/// <summary>
/// Represents electronic watches.
/// </summary>
public class ElectronicWatches : Watches
{
    /// <summary>
    /// Gets or sets the battery life in hours.
    /// </summary>
    public int BatteryLifeHours { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the watch has Bluetooth.
    /// </summary>
    public bool HasBluetooth { get; set; }

    /// <summary>
    /// Creates a new instance of the <see cref="ElectronicWatches"/> class.
    /// </summary>
    /// <param name="model">The model of the watch.</param>
    /// <param name="serialNumber">The serial number of the watch.</param>
    /// <param name="manufacturerId">The manufacturer ID.</param>
    /// <param name="batteryLifeHours">The battery life in hours.</param>
    /// <param name="hasBluetooth">Indicates if it has Bluetooth.</param>
    /// <returns>A new <see cref="ElectronicWatches"/> instance.</returns>
    public static ElectronicWatches Create(string model, string serialNumber, int manufacturerId, int batteryLifeHours, bool hasBluetooth)
    {
        return new ElectronicWatches
        {
            Model = model,
            SerialNumber = serialNumber,
            ManufacturerId = manufacturerId,
            BatteryLifeHours = batteryLifeHours,
            HasBluetooth = hasBluetooth
        };
    }

    /// <summary>
    /// Prints the electronic watch's details to the console.
    /// </summary>
    public override void PrintObject()
    {
        base.PrintObject();
        Console.WriteLine($"BatteryLifeHours: {BatteryLifeHours}, HasBluetooth: {HasBluetooth}");
    }
}