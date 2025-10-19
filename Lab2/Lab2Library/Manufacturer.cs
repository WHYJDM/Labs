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
    // ����� create ���������� ����� ��������� Manufacturer � ��������� 3 ��������� name, address, isAChildCompany
    // ��� ��� ����� ����������� ��� ������ �� ��������� ������� �������(create) ����� ��� ������� ������ � ���������� ������ �� �� ��� ���� ������ ���������
    {
        return new Manufacturer { Name = name, Address = address, IsAChildCompany = isAChildCompany };
        // return new Manufacturer ������� ����� ������ � ������, return ���������� ������ �� ���� ������
    }
    
    /// <summary>
    /// Prints the manufacturer details to the console.
    /// </summary>
    public void PrintObject()
    {
        Console.WriteLine($"Name: {Name}, Address: {Address}, IsAChildCompany: {IsAChildCompany}");
    }
}
//// ��� ��� �� ����� ������� ������ �� ������� "� �������� ���������� ������� ������������ ������ �� ���������� ������"