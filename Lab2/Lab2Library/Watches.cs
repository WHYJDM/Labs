using System.Xml.Serialization;

namespace Lab2Library;

public enum WatchesType
{
    Electronic,
    Mechanic,
    Tower
}

public class Watches
{
    private int Id { get; set; }

    [XmlAttribute]
    public string? Model { get; set; }

    public string? SerialNumber { get; set; }
    public WatchesType Type { get; set; }

    public static Watches Create(int id, string model, string serialNumber, WatchesType type)
    {
        return new Watches { Id = id, Model = model, SerialNumber = serialNumber, Type = type };
    }

    public void PrintObject()
    {
        Console.WriteLine($"Id: {Id}, Model: {Model}, SerialNumber: {SerialNumber}, Type: {Type}");
    }
}