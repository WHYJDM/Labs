namespace Lab2Library;

/// <summary>
/// Represents a manufacturer of watches.
/// </summary>
public class Manufacturer
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    private bool IsAChildCompany { get; set; }

    /// <summary>
    /// Creates a new instance of Manufacturer.
    /// </summary>
    public static Manufacturer Create(string name, string address, bool isAChildCompany)
    {
        return new Manufacturer { Name = name, Address = address, IsAChildCompany = isAChildCompany };
    }
    
    /// <summary>
    /// Prints the manufacturer details to the console.
    /// </summary>
    public void PrintObject()
    {
        Console.WriteLine($"Name: {Name}, Address: {Address}, IsAChildCompany: {IsAChildCompany}");
    }
}