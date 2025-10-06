using System;

namespace Lab1Library;

public enum WatchesType
{
    Electronic,
    Mechanic,
    Tower
}

public class Watches
{
    private int Id { get; set; }
    public string Name { get; set; }
    public string SerialNumber { get; set; }
    public WatchesType Type { get; set; }

    public static Watches Create(int id, string name, string serialNumber, WatchesType type)
    {
        return new Watches { Id = id, Name = name, SerialNumber = serialNumber, Type = type };
    }

    public void PrintObject()
    {
        Console.WriteLine($"Id: {Id}, Name: {Name}, SerialNumber: {SerialNumber}, Type: {Type}");
    }
}

public class Manufacturer
{
    public string Name { get; set; }
    public string Address { get; set; }
    private bool IsAChildCompany { get; set; }

    public static Manufacturer Create(string name, string address, bool isAChildCompany)
    {
        return new Manufacturer { Name = name, Address = address, IsAChildCompany = isAChildCompany };
    }

    public void PrintObject()
    {
        Console.WriteLine($"Name: {Name}, Address: {Address}, IsAChildCompany: {IsAChildCompany}");
    }
}
