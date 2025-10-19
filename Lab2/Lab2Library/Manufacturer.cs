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
    // метод create возвращает новый Ё «≈ћѕЋя– Manufacturer и принимает 3 аргумента name, address, isAChildCompany
    // так как метод статический это чертеж со встроеной кнопкой сделать(create) после оно создает обьект и возвращает ссылку на то где этот обьект находитс€
    {
        return new Manufacturer { Name = name, Address = address, IsAChildCompany = isAChildCompany };
        // return new Manufacturer создает новый обьект в пам€ти, return возвращает ссылку на этот объект
    }
    
    /// <summary>
    /// Prints the manufacturer details to the console.
    /// </summary>
    public void PrintObject()
    {
        Console.WriteLine($"Name: {Name}, Address: {Address}, IsAChildCompany: {IsAChildCompany}");
    }
}
//// это тут не нужно оставил просто по заданию "¬ качестве предметной области использовать классы из предыдущей работы"