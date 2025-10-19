using System;

namespace Lab7App;

/// <summary>
/// Represents a manufacturer entity.
/// </summary>
public class Manufacturer
{
    /// <summary>
    /// Gets or sets the unique identifier for the manufacturer.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the manufacturer.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the address of the manufacturer.
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the manufacturer is a child company.
    /// </summary>
    public bool IsAChildCompany { get; set; }

    /// <summary>
    /// Gets or sets the collection of watches produced by this manufacturer.
    /// </summary>
    public virtual ICollection<Watches> Watches { get; set; }

    /// <summary>
    /// Creates a new instance of the <see cref="Manufacturer"/> class.
    /// </summary>
    /// <param name="name">The name of the manufacturer.</param>
    /// <param name="address">The address of the manufacturer.</param>
    /// <param name="isAChildCompany">Indicates if it's a child company.</param>
    /// <returns>A new <see cref="Manufacturer"/> instance.</returns>
    public static Manufacturer Create(string name, string address, bool isAChildCompany)
    {
        return new Manufacturer { Name = name, Address = address, IsAChildCompany = isAChildCompany };
    }

    /// <summary>
    /// Prints the manufacturer's details to the console.
    /// </summary>
    public void PrintObject()
    {
        Console.WriteLine($"Id: {Id}, Name: {Name}, Address: {Address}, IsAChildCompany: {IsAChildCompany}");
    }
}