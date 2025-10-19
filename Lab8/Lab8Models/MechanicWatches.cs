using System;

namespace Lab8Models;

/// <summary>
/// Represents mechanic watches.
/// </summary>
public class MechanicWatches : Watches
{
    /// <summary>
    /// Gets or sets the movement type.
    /// </summary>
    public string MovementType { get; set; }

    /// <summary>
    /// Gets or sets the number of jewels.
    /// </summary>
    public int NumberOfJewels { get; set; }

    /// <summary>
    /// Creates a new instance of the <see cref="MechanicWatches"/> class.
    /// </summary>
    /// <param name="model">The model of the watch.</param>
    /// <param name="serialNumber">The serial number of the watch.</param>
    /// <param name="manufacturerId">The manufacturer ID.</param>
    /// <param name="movementType">The movement type.</param>
    /// <param name="numberOfJewels">The number of jewels.</param>
    /// <returns>A new <see cref="MechanicWatches"/> instance.</returns>
    public static MechanicWatches Create(string model, string serialNumber, int manufacturerId, string movementType, int numberOfJewels)
    {
        return new MechanicWatches
        {
            Model = model,
            SerialNumber = serialNumber,
            ManufacturerId = manufacturerId,
            MovementType = movementType,
            NumberOfJewels = numberOfJewels
        };
    }

    /// <summary>
    /// Prints the mechanic watch's details to the console.
    /// </summary>
    public override void PrintObject()
    {
        base.PrintObject();
        Console.WriteLine($"MovementType: {MovementType}, NumberOfJewels: {NumberOfJewels}");
    }
}